/*
 * Copyright (C) 2007 Chris Meadowcroft <crmeadowcroft@gmail.com>
 *
 * This file is part of CMPlayer, a free video player.
 * See http://sourceforge.net/projects/crmplayer for updates.
 *
 * CMPlayer is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * CMPlayer is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace DvdNavigatorCrm
{
    public class ProgramGroupChain
    {
        bool isEntry;
        int title;
        int programCount;
        int[] programStartCell;
        int cellCount;
        List<CellInformation> cells = new List<CellInformation>();
        float playbackTime;
        float fps;
        SortedList<int, int> audioStreams = new SortedList<int, int>();
        SortedList<int, int[]> subpictureStreams = new SortedList<int, int[]>();
        int nextPGC;
        int prevPGC;
        int upPGC;
        int[] palette = new int[16];

        public ProgramGroupChain(bool isEntry, int title)
        {
            this.isEntry = isEntry;
            this.title = title;
        }

        internal void Parse(IfoReader reader, int offset)
        {
            reader.SeekFromStart(offset + 2);
            this.programCount = reader.ReadByte();
            this.cellCount = reader.ReadByte();

            int hours, minutes, seconds, frames;
            reader.ReadTimingInfo(out hours, out minutes, out seconds, out frames, out this.fps);
            this.playbackTime = Convert.ToSingle(hours * 3600 + minutes * 60 + seconds) +
                Convert.ToSingle(frames) / this.fps;

            reader.SeekFromStart(offset + 0x0c);
            for (int audioIndex = 0; audioIndex < 8; audioIndex++)
            {
                int number = reader.ReadByte();
                if ((number & 0x80) != 0)
                {
                    this.audioStreams[audioIndex] = number & 0x07;
                }
                reader.SeekFromCurrent(1);
            }

            for (int subIndex = 0; subIndex < 32; subIndex++)
            {
                int number43 = reader.ReadByte();
                int numberWide = reader.ReadByte() & 0x1f;
                int numberLetterBox = reader.ReadByte() & 0x1f;
                int numberPanScan = reader.ReadByte() & 0x1f;
                if ((number43 & 0x80) != 0)
                {
                    this.subpictureStreams[subIndex] = new int[4] { number43 & 0x1f,
                            numberWide, numberLetterBox, numberPanScan };
                }
            }

            this.nextPGC = reader.ReadUInt16();
            this.prevPGC = reader.ReadUInt16();
            this.upPGC = reader.ReadUInt16();

            reader.SeekFromCurrent(2);
            for (int paletteIndex = 0; paletteIndex < 16; paletteIndex++)
            {
                this.palette[paletteIndex] = (int)reader.ReadUInt32();
            }

            int commandsOffset = offset + reader.ReadUInt16();
            int programMapOffset = offset + reader.ReadUInt16();
            int cellPlaybackOffset = offset + reader.ReadUInt16();
            int cellPositionOffset = offset + reader.ReadUInt16();

            this.programStartCell = new int[this.programCount];
            reader.SeekFromStart(programMapOffset);
            for (int startCellIndex = 0; startCellIndex < this.programCount; startCellIndex++)
            {
                this.programStartCell[startCellIndex] = reader.ReadByte();
            }

            reader.SeekFromStart(cellPlaybackOffset);
            for (int cellIndex = 0; cellIndex < this.cellCount; cellIndex++)
            {
                CellInformation cell = new CellInformation();
                cell.ParsePlayback(reader);
                this.cells.Add(cell);
            }

            reader.SeekFromStart(cellPositionOffset);
            for (int cellIndex = 0; cellIndex < this.cellCount; cellIndex++)
            {
                this.cells[cellIndex].ParsePosition(reader);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("Entry {0} Title {1} Programs {2} Cells {3} Time {4:f2} FPS {5}\n",
                this.isEntry, this.title, this.programCount, this.cellCount,
                this.playbackTime, this.fps);

            foreach (KeyValuePair<int, int> audioItem in this.audioStreams)
            {
                sb.AppendFormat("Audio Track {0} ID {1}\n", audioItem.Key + 1, audioItem.Value);
            }

            foreach (KeyValuePair<int, int[]> subpictureItem in this.subpictureStreams)
            {
                sb.AppendFormat("Sub Track {0} IDs: 4:3 {1}, Wide {2}, LetterBox {3}, PanScan {4}\n",
                    subpictureItem.Key + 1, subpictureItem.Value[0], subpictureItem.Value[1],
                    subpictureItem.Value[2], subpictureItem.Value[3]);
            }

            sb.AppendFormat("PGC Next {0} Prev {1} Up {2}\n", this.nextPGC, this.prevPGC, this.upPGC);

            sb.Append("Palette ");
            for (int paletteIndex = 0; paletteIndex < 16; paletteIndex++)
            {
                sb.AppendFormat("{0:X6} ", this.palette[paletteIndex]);
            }
            sb.Append("\n");

            sb.Append("Starting Cells ");
            for (int startCellIndex = 0; startCellIndex < this.programCount; startCellIndex++)
            {
                sb.AppendFormat("{0} ", this.programStartCell[startCellIndex]);
            }
            sb.Append("\n\n");

            for (int cellIndex = 0; cellIndex < this.cellCount; cellIndex++)
            {
                CellInformation cell = this.cells[cellIndex];
                sb.AppendFormat("Cell {0}\n", cellIndex + 1);
                sb.Append(cell.ToString());
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public bool IsEntry { get { return this.isEntry; } }
        public int Title { get { return this.title; } }

        public int ProgramCount { get { return this.programCount; } }
        public int GetProgramStartCell(int program) { return this.programStartCell[program - 1]; }

        public int CellCount { get { return this.cellCount; } }
        public CellInformation GetCell(int cell) { return this.cells[cell - 1]; }

        public float PlaybackTime { get { return this.playbackTime; } }
        public float FPS { get { return this.fps; } }
        public SortedList<int, int> AudioStreams { get { return this.audioStreams; } }
        public SortedList<int, int[]> SubpictureStreams { get { return this.subpictureStreams; } }
        public int NextPGC { get { return this.nextPGC; } }
        public int PrevPGC { get { return this.prevPGC; } }
        public int UpPGC { get { return this.upPGC; } }
        public int GetPaletteColor(int index) { return this.palette[index]; }
        public IList<int> Palette { get { return new List<int>(this.palette); } }
    }
}
