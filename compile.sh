#!/bin/bash

mcs -sdk:4.5 -out:ServUO.exe -d:MONO -d:Framework_4_0 -d:ServUO -optimize+ -unsafe -r:System,System.Configuration.Install,System.Data,System.Drawing,System.EnterpriseServices,System.Management,System.Security,System.ServiceProcess,System.Web,System.Web.Services,System.Windows.Forms,System.Xml,OpenUO.Core.dll,OpenUO.Ultima.dll,OpenUO.Ultima.Windows.Forms.dll,SevenZipSharp.dll -nowarn:219 -recurse:Server/*.cs

