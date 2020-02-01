@echo off
set path=C:\Windows\Microsoft.NET\Framework\v4.0.30319\
%path%csc.exe /t:winexe Design.cs Form.cs
