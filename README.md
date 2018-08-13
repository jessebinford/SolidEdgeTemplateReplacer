# Solid Edge Template Replacer
A simple Solid Edge draft background sheet replacer app.

This application allows for a user to easily replace the background sheets in an existing draft document with the background sheets in a template draft document.

There are currently two possible modes:
SEEC & Unmanaged

Unmanaged:
This mode is for use with standalone Solid Edge files that are not in any type of PDM system. Files are stored on a file path that is accessible locally.

The user has the option to either select the target draft (Draft needing the background sheets replaced) from a file location path, or pick from a list of already opened draft documents in Solid Edge. The user will also be required to select a template draft from a file location path, and it will be opened once they begin the replacement.

SEEC:
This mode is for Solid Edge Embedded Client, which is the integration for Teamcenter. 

In this mode the user specifies the ItemID and Rev for the template draft inside of Teamcenter. Once the user identifies these two fields and clicks go, they will be prompted to select an already opened draft document that needs the background sheets replaced.


During the sheet replacement, the user will be prompted to select a template background sheet for each background sheet in the target document. The user has the option to NOT replace a background sheet with a template if they so choose.


Latest Build Download:

https://github.com/jrbinford/SolidEdgeTemplateReplacer/releases/download/1.0/Solid.Edge.Template.Replacer.zip

This application was built on ST10 interops, and has been tested on ST9 and ST10.

This project uses SolidEdge Community for interops and application calls.

https://github.com/SolidEdgeCommunity
