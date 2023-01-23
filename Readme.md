# - dotnet publish -c Release
# - docker build -t scanner-jeff-image -f Dockerfile .
# - docker create --name scanner-jeff scanner-jeff-image
# - docker run -it -v ~/toscan:/App/toscan scanner-jeff-image

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
 


There are several common searching algorithms used in virus scanners, including:

Signature-based scanning: This method involves comparing the files on a computer to a database of known virus signatures. If a match is found, the file is flagged as potentially malicious.
Heuristic-based scanning: This method uses algorithms that try to identify patterns or behaviors that are commonly associated with malware.
Sandboxing: This method involves running a file in a virtual environment and monitoring its behavior to see if it exhibits any characteristics of malware.
Artificial Intelligence and Machine Learning: This method uses algorithms that can learn from the data and improve its performance over time.
Behavioral-based scanning: This method involves monitoring the behavior of a file on a computer and comparing it to a database of known malicious behaviors.
Emulation-based scanning: This method involves running a file in a simulated environment, monitoring its behavior and comparing it to a set of predefined rules to determine if it is malicious.


