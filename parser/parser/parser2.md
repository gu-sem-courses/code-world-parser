#### The __```parser```__ Structure
This is where a srcML xml document is filtered and parsed into a constrained JSON file.
a. ```bin```
b. ```obj```
c. ```Properties```
d. __```JsonParser.cs```__
>This file has the single responsibility of parsing xml into json strings.

e. __```Parser.cs```__
>This file fetches the srcML.xml file in the outbox and begins the process of filtering it before producing a json file.
>```Parser.cs``` uses the following files:
>- ```JsonParser.cs```
>- ```SrcMLFilter.cs```

f. __```SrcMLFilter.cs```__
>The ```SrcMLFilter.cs``` filters through a srcML produced xml file and retrieves the needed data.
>
>__Class__(es)
    - __attribute__(s)
    - __method__(s)
    - __interface__(s)
    - __superclass__(es)
    - __subclass__(es)
    - __association__(s)

