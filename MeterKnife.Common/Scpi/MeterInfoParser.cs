﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using NKnife.Interface;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Common.Scpi
{
    public class MeterInfoParser : IParser<FileInfo, List<ScpiSubject>>
    {
        public string Brand { get; private set; }

        public string Name { get; private set; }

        public bool TryParse(FileInfo fileInfo, out List<ScpiSubject> scpiSubjectList)
        {
            if (fileInfo == null)
                throw new ArgumentNullException("fileInfo");

            if (!fileInfo.Exists)
            {
                scpiSubjectList = null;
                return false;
            }
            var xmldoc = new XmlDocument();
            xmldoc.Load(fileInfo.FullName);

            var meterinfoElement = xmldoc.SelectSingleNode("//meterinfo") as XmlElement;
            if (meterinfoElement == null)
            {
                scpiSubjectList = null;
                return false;
            }
            Brand = meterinfoElement.GetAttribute("brand");
            Name = meterinfoElement.GetAttribute("name");

            var node = xmldoc.SelectSingleNode("//scpigroups");
            var scpigroups = node as XmlElement;
            if (scpigroups == null)
            {
                scpiSubjectList = null;
                return false;
            }
            scpiSubjectList = new List<ScpiSubject>();

            foreach (var subjectNode in scpigroups.ChildNodes)
            {
                if (!(subjectNode is XmlElement))
                    continue;
                var scpiSubject = new ScpiSubject();
                var ele = subjectNode as XmlElement;
                scpiSubject.Description = ele.GetAttribute("description");

                var initGroupElement = ele.SelectSingleNode("//group[@way='init']") as XmlElement;
                scpiSubject.Preload = new ScpiGroup();
                if (initGroupElement != null)
                {
                    foreach (XmlElement scpiElement in initGroupElement.ChildNodes)
                    {
                        var scpiCommand = ScpiCommand.Parse(scpiElement);
                        if (scpiCommand == null)
                            continue;
                        scpiSubject.Preload.AddLast(scpiCommand);
                    }
                }

                var collectGroupElement = ele.SelectSingleNode("//group[@way='collect']") as XmlElement;
                scpiSubject.Collect = new ScpiGroup();
                if (collectGroupElement != null)
                {
                    foreach (XmlElement scpiElement in collectGroupElement.ChildNodes)
                    {
                        var scpiCommand = ScpiCommand.Parse(scpiElement);
                        if (scpiCommand == null)
                            continue;
                        scpiSubject.Collect.AddLast(scpiCommand);
                    }
                }
                scpiSubjectList.Add(scpiSubject);
            }

            return true;
        }
    }
}