FileReaderApp

Simple consol app for reading txt, xml and json files, depending on what the user chooses.  
You can also choose to read encrypted files (the only encription is it just reverses the text), or restrict what is shown based on the user's role (2 roles: admin and employee).

What it does

- Reads normal files
- Reads reverse encrypted files (only reverse encryption)
- Reads fils with access control (admin sees everything, employee sees some parts)
- Works for .txt, .xml and .json
- CLI menu to chose w what to read and how
- After it reads a file, you can read another right away without restarting

How to use

1. Clone this repo
2. Open in Visual Studio
3. Run the project (Program.cs)
4. Answer prompts in console

If you pick encryption, you can't use roles (and vice versa). The app warns about it

File structure

There’s stuff like:

- Files/txt/sample.txt  
- Files/txt/encrypted.txt  
- Files/txt/role.txt  
- Same for XML and JSON in their folders

Roles

- Admin: sees everything
- Employee: sees [PUBLIC] and [SHARED] (in txt), or shared sections in XML/JSON

Encryption

Only  reversing the text. So `"hello"` becomes `"olleh"`.

Versions (tags)

Each version has it's tag:
v1 - plain text  
v2 - xml  
v3 - encrypted txt  
v4 - xml + role  
v5 - encrypted xml  
v6 - txt + role  
v7 - json  
v8 - encrypted json  
v9 - json + role  
v10 - CLI loop (asks what and how to read)

How to run
dotnet run --project FileReaderApp
