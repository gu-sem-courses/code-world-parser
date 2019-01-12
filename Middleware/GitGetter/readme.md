There are 4 classes used in the GitGetter and they are explained here.


PROGRAM.CS
Program.cs is the main file and it has the code for getting files from gitlab or calling
GithubGetter incase the repository is stored on github. After the relevant files have
been downloaded it calls on srcml to generate a xml file based on the repository.


GITHUBGETTER.CS
GithubGetter is the class that has all the code for retrieving files from github
and it is used in case the repository is on github rather than gitlab. The class starts by
retrieving the main directory and serializing it as a github object. It then goes through
the content of the folder and if it is another folder then it goes through it in the same way
and if it is an java file then its contents is retrieved and put into a java file.

GITHUB.CS
Github.cs is used to serialize the directories in Github repositories.


TREE.CS
Tree.cs is used to serialize the directories in Gitlab repositories. 