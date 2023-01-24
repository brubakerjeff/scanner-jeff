##  Scanner - Jeff


 The proposed solution is to mount a local directory into the docker image, where a script will automatically extract and process any compressed files (e.g. tar.gz or zip).
 Using parellel processing with a set of filters, the script will flag any suspicious files unless they pass a safe filter and place them in the quarantine folder.

The current filters look at name and hash mapping. With the use of object-oriented programming and its extensibility, it is possible to add other filters without breaking the pattern. Algorithms such as more advanced matching algorithms or interfacing with simulated environment such as a cyber range for further analysis.

## Assumptions:
- Client has docker available
- Items of interest are stored in ~/toscan

To run the current environment the following commands are recommended.
```` 
   dotnet publish -c Release
   docker build -t scanner-jeff-image -f Dockerfile .
   docker create --name scanner-jeff scanner-jeff-image
   docker run -it -v ~/toscan:/App/toscan scanner-jeff-image
````


Please carefully consider the user story when planning your approach. For example, ease of deployment would be very important to our users.  
 
We will test your solution against a large dataset (at least 100,000 files) of mixed file types, including but not limited to: documents of multiple types (pdf, Word, Excel, LibreOffice, etc.), compressed files, executables, and various plain-text formats.  
 
We would prefer that you share the code with us by pushing it to a public repository in the hosting provider of your choosing. If you do not want to upload it to a public repository, then you can zip up the repository folder on your machine and send it to us via email. If you go that route, please make sure that the .git folder is included.  
 
List any assumptions you made in the process and be prepared to discuss your design. If you have any questions, please contact us and we will do our best to clarify anything that is unclear.  
 
# Rules:  
 
You can use any libraries you like.  
 
I would prefer that you use .NET Core
 
The code should be stored in a git repository. 
 
Goals:  
 
Minimize false positives.  
 
Performance, 100k files in 5 minutes.  
 


