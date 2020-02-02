@echo off
set path=C:\Windows\Microsoft.NET\Framework\v4.0.30319\
:Re
cls
%path%csc.exe /t:winexe Design.cs Form.cs
Form.exe
pause
goto Re