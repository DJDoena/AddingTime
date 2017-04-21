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
using System.IO;
using System.Text;

namespace DvdNavigatorCrm
{
    public class DvdTitleSet : IDisposable
    {
        public const string VMG_ID = "DVDVIDEO-VMG";
        public const string VTS_ID = "DVDVIDEO-VTS";

        bool disposed;
        IfoReader ifoReader;
        int videoTsFileOffset = -1;
        int vtsStartSector = -1;
        int lastSectorOfTitleSet;
        int lastSectorOfIfo;
        int startSectorMenuVob;
        int startSectorTitleVob;
        int titleCount;
        List<List<PartOfTitle>> titleParts;
        List<DvdTitle> titles;
        ProgramGroupChain[] chains;
        int audioTrackCount;
        AudioAttributes[] audioAttributes = new AudioAttributes[8];
        int subTrackCount;
        SubpictureAttributes[] subAttributes = new SubpictureAttributes[32];

        public DvdTitleSet(string fileName)
        {
            this.ifoReader = new IfoReader(fileName);
        }

        public int TitleCount { get { return this.titleCount; } }
        public IList<PartOfTitle> GetTitleParts(int title) { return this.titleParts[title - 1].AsReadOnly(); }
        public IList<DvdTitle> Titles { get { return this.titles.AsReadOnly(); } }
        public int ChainCount { get { return this.chains.Length; } }
        public ProgramGroupChain GetChain(int chain) { return this.chains[chain - 1]; }
        public int AudioTrackCount { get { return this.audioTrackCount; } }
        public AudioAttributes GetAudioAttributes(int index) { return this.audioAttributes[index - 1]; }
        public int SubtitleTrackCount { get { return this.subTrackCount; } }
        public SubpictureAttributes GetSubtitleAttributes(int index) { return this.subAttributes[index - 1]; }
        public int LastSectorOfTitleSet { get { return this.lastSectorOfTitleSet; } }
        public int LastSectorOfIfo { get { return this.lastSectorOfIfo; } }
        public int StartSectorMenuVob { get { return this.startSectorMenuVob; } }
        public int StartSectorTitleVob { get { return this.startSectorTitleVob; } }
        public int VtsStartSector { get { return this.vtsStartSector; } }
        public int VideoTsFileOffset
        {
            get { return this.videoTsFileOffset; }
            set { this.videoTsFileOffset = value; }
        }

        public bool IsValidTitleSet
        {
            get
            {
                this.ifoReader.SeekFromStart(0);
                byte[] tempBuffer = new byte[VTS_ID.Length];
                this.ifoReader.Read(tempBuffer, VTS_ID.Length);
                if (Encoding.ASCII.GetString(tempBuffer, 0, VTS_ID.Length) != VTS_ID)
                {
                    return false;
                }
                return true;
            }
        }

        public string FileName
        {
            get
            {
                return this.ifoReader.FileName;
            }
        }

        public void Parse()
        {
            this.FindStartSector();
            this.ParsePTT();
            this.ParsePGCI();
            this.ParseAudioAndSubs();
            this.BuildTitles();
        }

        void FindStartSector()
        {
            string videoTsIfoPath = Path.Combine(Path.GetDirectoryName(FileName), "VIDEO_TS.IFO");
            if (File.Exists(videoTsIfoPath))
            {
                int currentVts = int.Parse(Path.GetFileNameWithoutExtension(FileName).Substring(4, 2));
                IfoReader vtsReader = new IfoReader(videoTsIfoPath);
                int ttSrptOffset = (int)vtsReader.ReadUInt32(0xc4) * 0x800;
                vtsReader.SeekFromStart(ttSrptOffset);
                int numberOfEntries = vtsReader.ReadUInt16();
                vtsReader.SeekFromCurrent(2);
                for (int index = 0; index < numberOfEntries; index++)
                {
                    vtsReader.SeekFromCurrent(10);
                    int vtsNumber = vtsReader.ReadByte();
                    vtsReader.ReadByte();
                    if (currentVts == vtsNumber)
                    {
                        this.vtsStartSector = (int)vtsReader.ReadUInt32();
                        break;
                    }
                }
            }
        }

        void ParsePTT()
        {
            this.lastSectorOfTitleSet = (int)this.ifoReader.ReadUInt32(0x0c);
            this.lastSectorOfIfo = (int)this.ifoReader.ReadUInt32(0x1c);
            this.startSectorMenuVob = (int)this.ifoReader.ReadUInt32(0xc0);
            this.startSectorTitleVob = (int)this.ifoReader.ReadUInt32(0xc4);
            uint pttSrptSector = this.ifoReader.ReadUInt32(0xc8);
            int pttSrptOffset = (int)pttSrptSector * 0x800;

            this.titleCount = this.ifoReader.ReadUInt16(pttSrptOffset);
            this.ifoReader.SeekFromCurrent(2);

            this.titleParts = new List<List<PartOfTitle>>(titleCount);

            int[] pttOffsets = new int[this.titleCount + 1];
            pttOffsets[this.titleCount] = (int)this.ifoReader.ReadUInt32() + 1;
            for (int index = 0; index < this.titleCount; index++)
            {
                pttOffsets[index] = (int)this.ifoReader.ReadUInt32();
            }

            int nextPtt = pttSrptOffset + pttOffsets[0];
            this.ifoReader.SeekFromStart(nextPtt);
            for (int pttIndex = 0; pttIndex < this.titleCount; pttIndex++)
            {
                long endOfPtt = pttSrptOffset + pttOffsets[pttIndex + 1];
                List<PartOfTitle> ptt = new List<PartOfTitle>();
                while (nextPtt < endOfPtt)
                {
                    ptt.Add(new PartOfTitle(this.ifoReader.ReadUInt16(), this.ifoReader.ReadUInt16()));
                    nextPtt += 4;
                }
                titleParts.Add(ptt);
            }
        }

        void ParsePGCI()
        {
            uint pgciSector = this.ifoReader.ReadUInt32(0xcc);
            int pgciOffset = (int)pgciSector * 0x800;
            if (pgciOffset > this.ifoReader.Length)
            {
                this.titleCount = 0;
                this.chains = new ProgramGroupChain[0];
                return;
            }

            int chainCount = this.ifoReader.ReadUInt16(pgciOffset);
            this.chains = new ProgramGroupChain[chainCount];
            this.ifoReader.SeekFromCurrent(2);
            long endPgci = pgciOffset + this.ifoReader.ReadUInt32() + 1L;

            uint[] chainOffsets = new uint[chainCount];
            for (int index = 0; index < chainCount; index++)
            {
                uint category = this.ifoReader.ReadUInt32();
                chainOffsets[index] = this.ifoReader.ReadUInt32();
                this.chains[index] = new ProgramGroupChain((category & 0x80000000) != 0,
                    Convert.ToInt32((category & 0x7f000000) >> 24));
            }
            for (int index = 0; index < chainCount; index++)
            {
                this.chains[index].Parse(this.ifoReader, pgciOffset + (int)chainOffsets[index]);
            }
        }

        void ParseAudioAndSubs()
        {
            this.ifoReader.SeekFromStart(0x202);
            this.audioTrackCount = this.ifoReader.ReadUInt16();
            for (int audioIndex = 0; audioIndex < this.audioTrackCount; audioIndex++)
            {
                this.ifoReader.SeekFromStart(0x204 + audioIndex * 8);
                AudioAttributes audio = new AudioAttributes();
                audio.TrackId = audioIndex + 1;
                int codingByte = this.ifoReader.ReadByte();
                audio.CodingMode = (AudioCodingMode)(codingByte >> 5);
                int samplingByte = this.ifoReader.ReadByte();
                audio.Channels = (samplingByte & 0x07) + 1;
                if ((codingByte & 0x0c) != 0)
                {
                    byte[] lang = new byte[2];
                    if (this.ifoReader.Read(lang, 2) == 2)
                    {
                        audio.Language = Encoding.ASCII.GetString(lang);
                    }
                }
                if (audio.Language == null)
                {
                    audio.Language = string.Empty;
                }
                this.ifoReader.SeekFromStart(0x204 + audioIndex * 8 + 5);
                audio.CodeExtension = (AudioCodeExtension)(this.ifoReader.ReadByte());

                audioAttributes[audioIndex] = audio;
            }

            this.ifoReader.SeekFromStart(0x254);
            this.subTrackCount = this.ifoReader.ReadUInt16();
            for (int subIndex = 0; subIndex < this.subTrackCount; subIndex++)
            {
                this.ifoReader.SeekFromStart(0x256 + subIndex * 6);
                SubpictureAttributes subs = new SubpictureAttributes();
                subs.TrackId = subIndex + 1;
                int codingByte = this.ifoReader.ReadByte();
                this.ifoReader.SeekFromCurrent(1);
                if ((codingByte & 0x03) != 0)
                {
                    byte[] lang = new byte[2];
                    if (this.ifoReader.Read(lang, 2) == 2)
                    {
                        subs.Language = Encoding.ASCII.GetString(lang);
                    }
                }
                this.ifoReader.SeekFromStart(0x256 + subIndex * 6 + 5);
                subs.CodeExtension = (SubpictureCodeExtension)(this.ifoReader.ReadByte());
                this.subAttributes[subIndex] = subs;
            }
        }

        void BuildTitles()
        {
            this.titles = new List<DvdTitle>(this.titleCount);
            List<IList<int>> allCells = new List<IList<int>>();
            for (int titleIndex = 1; titleIndex <= this.titleCount; titleIndex++)
            {
                DvdTitle newTitle = new DvdTitle(this, titleIndex);
                allCells.Add(newTitle.CellSectorList);
                this.titles.Add(newTitle);
            }

            List<int> weakTitles = new List<int>();
            // dispose of any titles which are just empty, matches, or subsets of another title
            for (int titleIndex = 0; titleIndex < allCells.Count; titleIndex++)
            {
                IList<int> titleCells = allCells[titleIndex];
                if (titleCells.Count == 0)
                {
                    weakTitles.Add(titleIndex);
                    continue;
                }

                for (int testIndex = 0; testIndex < this.titles.Count; testIndex++)
                {
                    if (testIndex == titleIndex)
                    {
                        continue;
                    }

                    IList<int> testCells = allCells[testIndex];
                    if ((testCells.Count == titleCells.Count) && (testIndex < titleIndex))
                    {
                        bool match = true;
                        for (int cellIndex = 0; cellIndex < testCells.Count; cellIndex++)
                        {
                            if (testCells[cellIndex] != titleCells[cellIndex])
                            {
                                match = false;
                                break;
                            }
                        }
                        if (match)
                        {
                            weakTitles.Add(titleIndex);
                            break;
                        }
                    }
                    else if (testCells.Count > titleCells.Count)
                    {
                        bool subset = false;
                        for (int cellIndex = 0; cellIndex < testCells.Count - titleCells.Count + 1; cellIndex++)
                        {
                            if (testCells[cellIndex] == titleCells[0])
                            {
                                subset = true;
                                for (int innerCellIndex = 1; innerCellIndex < titleCells.Count; innerCellIndex++)
                                {
                                    if (testCells[cellIndex + innerCellIndex] != titleCells[innerCellIndex])
                                    {
                                        subset = false;
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        if (subset)
                        {
                            weakTitles.Add(titleIndex);
                            break;
                        }
                    }
                }
            }
            for (int weakIndex = 0; weakIndex < weakTitles.Count; weakIndex++)
            {
                this.titleCount--;
                this.titles.RemoveAt(weakTitles[weakIndex] - weakIndex);
                this.titleParts.RemoveAt(weakTitles[weakIndex] - weakIndex);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\n", this.ifoReader.FileName);
            sb.AppendFormat("VTS Start Sector 0x{0:x}\n", this.vtsStartSector);
            sb.AppendFormat("Last Sector Title Set 0x{0:x}\n", this.lastSectorOfTitleSet);
            sb.AppendFormat("Last Sector IFO 0x{0:x}\n", this.lastSectorOfIfo);
            sb.AppendFormat("Start Sector Menu VOB 0x{0:x}\n", this.startSectorMenuVob);
            sb.AppendFormat("Start Sector Title VOB 0x{0:x}\n", this.startSectorTitleVob);

            sb.AppendFormat("{0} Titles\n\n", this.titleCount);
            for (int titleIndex = 0; titleIndex < this.titleCount; titleIndex++)
            {
                List<PartOfTitle> ptt = this.titleParts[titleIndex];
                sb.AppendFormat("Title {0}\n\n", titleIndex + 1);
                foreach (PartOfTitle pot in ptt)
                {
                    sb.AppendFormat("Chain {0} Program {1}\n", pot.ProgramChain, pot.Program);
                }
                sb.Append("\n");
            }

            sb.AppendFormat("{0} Chains\n\n", this.chains.Length);
            for (int index = 0; index < this.chains.Length; index++)
            {
                sb.AppendFormat("Chain {0}\n\n", index + 1);
                sb.Append(this.chains[index].ToString());
                sb.Append("\n");
            }

            if (this.audioTrackCount != 0)
            {
                sb.Append("\nAudio Tracks ");
                for (int index = 0; index < this.audioTrackCount; index++)
                {
                    AudioAttributes audio = this.audioAttributes[index];
                    if (audio.CodeExtension == AudioCodeExtension.Unspecified)
                    {
                        sb.AppendFormat(" {0} ({1} {2} {3}ch)", index + 1,
                            audio.CodingMode, DvdLanguageCodes.GetLanguageText(audio.Language),
                            audio.Channels);
                    }
                    else
                    {
                        sb.AppendFormat(" {0} ({1} {2} {3}ch {4})", index + 1,
                            audio.CodingMode, DvdLanguageCodes.GetLanguageText(audio.Language),
                            audio.Channels, audio.CodeExtension);
                    }
                }
                sb.Append("\n");
            }

            if (this.subTrackCount != 0)
            {
                sb.Append("\nSubtitle Tracks ");
                for (int index = 0; index < this.subTrackCount; index++)
                {
                    SubpictureAttributes subs = this.subAttributes[index];
                    if (subs.CodeExtension == SubpictureCodeExtension.UnSpecified)
                    {
                        sb.AppendFormat(" {0} ({1})", index + 1,
                            DvdLanguageCodes.GetLanguageText(subs.Language));
                    }
                    else
                    {
                        sb.AppendFormat(" {0} ({1} {2})", index + 1,
                            DvdLanguageCodes.GetLanguageText(subs.Language), subs.CodeExtension);
                    }
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DvdTitleSet()
        {
            Dispose(false);
        }
    }
}
