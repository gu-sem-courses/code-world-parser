There are 4 classes used in the GitGetter and they are explained here.


Program.cs
Program.cs is the main file and serves as the controller. This class is the one that decides if GitHubGetter or GitLabGetter is to be 
used to retrieve the repository. It choses this based on a argument given when it is started. The repo it will retrieve is also given in these
arguments. After GitHubGetter or GitLabGetter have finished Program passes the resulting file to srcml which generates a Xml file based 
on the retrieved repo. Program also stores some general methods which can be used by classes which retrieve Git repositories from a website.(
Current examples. GitLabGetter, GitHubGetter)
GithubGetter incase the repository is stored on github. After the relevant files have
been downloaded it calls on srcml to generate a xml file based on the repository.

GitLabGetter.cs
GitLabGetter is the class that is responsible for getting the relevant files from Gitlab incase the repo that the user wants is on there. 
The class starts by retrieving the main directory and serializing it as a GitLabObject. It then goes through
the content of the folder and if there is one or more folder then they are serialized in the same way and any folders inside them undergo the 
process. If a file of the specified type is found in any of these folders then it is retrieved and its contents are added to a file of 
the same type.


GitHubGetter.cs
GitHubGetter is the class that is responsible for getting the relevant files from Github incase the repo that the user wants is on there. 
The class starts by retrieving the main directory and serializing it as a GitHubObject. It then goes through
the content of the folder and if there is one or more folder then they are serialized in the same way and any folders inside them undergo the 
process. If a file of the specified type is found in any of these folders then it is retrieved and its contents are added to a file of 
the same type.

GitHubObject.cs
Tree.cs is a used to create a object that can contain the neccessary values that GitLabGetter recieves from Gitlab for for its requests.
This data is then used to make more requests to GitLab and the data from those requests are then used to create more GitLabObjects.
This then repeats until all the relevant data from GitLab has been recieved. 


GitLabObject.cs
GitlabObject is a used to create a object that can contain the neccessary values that GitLabGetter recieves from Gitlab for for its requests.
This data is then used to make more requests to GitLab and the data from those requests are then used to create more GitLabObjects.
This then repeats until all the relevant data from GitLab has been recieved. 