# YAMLtoCS.py - Generates C# code for IRC Numerics from numerics.yaml
# Author: Joey Pollack

import yaml

# values table structure:
# name: numeric name
# numeric: number
# origin: where the numeric was found
# when: release version or announced date
# contact: point of contact associated with the numeric
# information: url where to find more information
# format: format of the numeric data
# comment: "comments, history etc"
# seealso: number

class Numeric:
    def __init__(this, name, value):
        this.name = name;
        this.values = { value }
        this.comment = ""
        this.format = "";
        this.seealso = "";
        this.lowest = value;

    def AddNum(this, num):
        if (num < this.lowest):
            this.lowest = num

        this.values.add(num)

Numerics = dict()

with open("numerics.yaml", 'r') as numericsFile:
    fileAll = numericsFile.read()
    doc = yaml.load(fileAll)
    for v in doc['values']:


        cleanName = str(v['name']).replace(' ', '_')
        numeric = str(v['numeric'])

        if (cleanName in Numerics):
            Numerics[cleanName].AddNum(numeric)

        else:
            Numerics[cleanName] = Numeric(cleanName, numeric)


        try:
            nformat = str(v['format'])
            Numerics[cleanName].format += nformat + "\n"
        except:
            pass

        try:
            comment = str(v['comment'])
            Numerics[cleanName].comment += comment + "\n"
        except:
            pass

        try:
            information = str(v['information'])
            Numerics[cleanName].comment += information + "\n"
        except:
            pass

        try:
            seealso = str(v['seealso'])
            Numerics[cleanName].seealso += seealso + "\n"
        except:
            pass


# C# ENTRY EXAMPLE:
# /// <summary>
# /// <client> :Welcome to the Internet Relay Network <nick>!<user>@<host>
# /// The first message sent after client registration. The text used varies widely
# /// </summary>
# // public const int RPL_WELCOME = 001;
# public static readonly Numeric RPL_WELCOME = new Numeric("RPL_WELCOME", 001);

# first put Numerics into a list and then sort by first numeric value.
# this should give a cleaner file

listNums = list(Numerics.values())
listNums.sort(key=lambda x: x.lowest, reverse=False)


with open ("numerics.cs", 'w') as csFile:
    for n in listNums:

        csFile.write("\n\n")
        csFile.write("/// <summary>\n")

        if (n.format != ""):
            csFile.write("/// " + n.format.replace("\n", "\n/// ") + "\n")

        if (n.comment != ""):
            csFile.write("/// " + n.comment.replace("\n", "\n/// ") + "\n")

        if (n.seealso != ""):
            csFile.write("/// See also: " + n.seealso.replace("\n", "\n/// ") + "\n")

        csFile.write("/// </summary>" + "\n")
        csFile.write("public static readonly Numeric " + n.name + " = new Numeric(\"" + n.name + "\"")

        for s in n.values:
            csFile.write(", " + s)

        csFile.write(");")

print("DONE")
