# Toolkit.NET
Toolkit made by Bc. Ján Janušek.


<b>﻿# Toolkit Features:</b>

<b>Mapper</b> -> I made own automapper which is good if you need automapper which is very simple to use and is strongly customizable.
You can see how to use this mapper <a href="https://github.com/shoxik/Toolkit.NET/blob/master/Toolkit.NET/Program.cs">here</a>

<b>Regex parser</b> -> extension class for Regex, is very easy to use 
if you need to parse some data from big string which can be splitted into rows.


<b>﻿# Extensions classes:</b> 

Format from string is extended
<b>from</b> Format("{0}", value) <b>to</b> FormatString("[[0]]", value), FormatString("<0>", value), FormatString("|0|", value)
you can set your own brackets

I made this extension for those who need put some values into string which 
already contains original string Format brackets {} such as HTML file with style.
