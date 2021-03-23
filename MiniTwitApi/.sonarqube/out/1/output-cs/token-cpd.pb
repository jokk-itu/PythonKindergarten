Ã
\/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/CustomAuthenticationStateProvider.cs
	namespace

 	
MiniTwitApi


 
.

 
Client

 
{ 
public 

class -
!CustomAuthenticationStateProvider 2
:3 4'
AuthenticationStateProvider5 P
{ 
private 
readonly 

HttpClient #
_httpClient$ /
;/ 0
public -
!CustomAuthenticationStateProvider 0
(0 1

HttpClient1 ;

httpClient< F
)F G
{ 	
_httpClient 
= 

httpClient $
;$ %
} 	
public 
override 
async 
Task "
<" #
AuthenticationState# 6
>6 7'
GetAuthenticationStateAsync8 S
(S T
)T U
{ 	
try 
{ 
var 
response 
= 
await $
_httpClient% 0
.0 1
GetStringAsync1 ?
(? @
$str@ F
)F G
;G H
var 
user 
= 
JsonSerializer )
.) *
Deserialize* 5
<5 6
UserDTO6 =
>= >
(> ?
response? G
)G H
;H I
if 
( 
user 
is 
null  
)  !
return 
new 
AuthenticationState 2
(2 3
new3 6
ClaimsPrincipal7 F
(F G
)G H
)H I
;I J
var 
identity 
= 
new "
ClaimsIdentity# 1
(1 2
new2 5
[6 7
]7 8
{ 
new   
Claim   
(   

ClaimTypes   (
.  ( )
Name  ) -
,  - .
user  / 3
.  3 4
Username  4 <
)  < =
,  = >
new!! 
Claim!! 
(!! 

ClaimTypes!! (
.!!( )
Email!!) .
,!!. /
user!!0 4
.!!4 5
Email!!5 :
)!!: ;
}"" 
,"" 
$str"" 
)""  
;""  !
var$$ 
	principal$$ 
=$$ 
new$$  #
ClaimsPrincipal$$$ 3
($$3 4
identity$$4 <
)$$< =
;$$= >
return%% 
new%% 
AuthenticationState%% .
(%%. /
	principal%%/ 8
)%%8 9
;%%9 :
}&& 
catch'' 
('' 
	Exception'' 
e'' 
)'' 
{(( 
return)) 
new)) 
AuthenticationState)) .
()). /
new))/ 2
ClaimsPrincipal))3 B
())B C
)))C D
)))D E
;))E F
}** 
}++ 	
},, 
}-- Ü	
W/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/Models/Abstract/IFollowModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 
Models #
.# $
Abstract$ ,
{ 
public 

	interface 
IFollowModel !
{ 
public 
Task 
< 
bool 
> 

FollowUser $
($ %
string% +

myUsername, 6
,6 7
string8 >
followerUsername? O
)O P
;P Q
public 
Task 
< 
bool 
> 
UnfollowUser &
(& '
string' -

myUsername. 8
,8 9
string: @
followerUsernameA Q
)Q R
;R S
public

 
Task

 
<

 
bool

 
>

 

IsFollowed

 $
(

$ %
string

% +
whoUsername

, 7
,

7 8
string

9 ?
whomUsername

@ L
)

L M
;

M N
} 
} ı
X/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/Models/Abstract/IMessageModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 
Models #
.# $
Abstract$ ,
{ 
public 

	interface 
IMessageModel "
{		 
public

 
Task

 
<

 
ICollection

 
<

  

MessageDTO

  *
>

* +
>

+ ,
GetMessages

- 8
(

8 9
string

9 ?
path

@ D
)

D E
;

E F
public 
Task 
< 
bool 
> 
PostMessage %
(% &
CreateMessage& 3
message4 ;
,; <
string= C
usernameD L
)L M
;M N
} 
} û
U/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/Models/Abstract/IUserModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 
Models #
.# $
Abstract$ ,
{ 
public 

	interface 

IUserModel 
{		 
public

 
Task

 
<

 
bool

 
>

 
RegisterUser

 &
(

& '
CreateUserDTO

' 4
user

5 9
)

9 :
;

: ;
public 
Task 
< 
bool 
> 
	LoginUser #
(# $
LoginUserDTO$ 0
user1 5
)5 6
;6 7
public 
Task 
< 
UserDTO 
> 
GetLoggedInUser ,
(, -
)- .
;. /
} 
} Å(
M/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/Models/FollowModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 
Models #
{ 
public 

class 
FollowModel 
: 
IFollowModel +
{ 
private 
readonly 

HttpClient #
_client$ +
;+ ,
public 
FollowModel 
( 

HttpClient %
client& ,
), -
{ 	
_client 
= 
client 
; 
} 	
public 
async 
Task 
< 
bool 
> 

FollowUser  *
(* +
string+ 1

myUsername2 <
,< =
string> D
followerUsernameE U
)U V
{ 	
var 
json 
= 
JsonSerializer %
.% &
	Serialize& /
(/ 0
new0 3
Follow4 :
(: ;
); <
{ 
ToFollow 
= 
followerUsername +
} 
) 
; 
var 
data 
= 
new 
StringContent (
(( )
json) -
,- .
Encoding/ 7
.7 8
UTF88 <
,< =
$str> P
)P Q
;Q R
var 
response 
= 
await  
_client! (
.( )
	PostAsync) 2
(2 3
$"3 5
/fllws/5 <
{< =

myUsername= G
}G H
"H I
,I J
dataK O
)O P
;P Q
HttpFailureHelper 
. 
HandleStatusCode .
(. /
response/ 7
)7 8
;8 9
return   
response   
.   
IsSuccessStatusCode   /
;  / 0
}!! 	
public## 
async## 
Task## 
<## 
bool## 
>## 
UnfollowUser##  ,
(##, -
string##- 3

myUsername##4 >
,##> ?
string##@ F
followerUsername##G W
)##W X
{$$ 	
var%% 
json%% 
=%% 
JsonSerializer%% %
.%%% &
	Serialize%%& /
(%%/ 0
new%%0 3
Follow%%4 :
(%%: ;
)%%; <
{&& 

ToUnfollow'' 
='' 
followerUsername'' -
}(( 
)(( 
;(( 
var)) 
data)) 
=)) 
new)) 
StringContent)) (
())( )
json))) -
,))- .
Encoding))/ 7
.))7 8
UTF8))8 <
,))< =
$str))> P
)))P Q
;))Q R
var** 
response** 
=** 
await**  
_client**! (
.**( )
	PostAsync**) 2
(**2 3
$"**3 5
/fllws/**5 <
{**< =

myUsername**= G
}**G H
"**H I
,**I J
data**K O
)**O P
;**P Q
HttpFailureHelper++ 
.++ 
HandleStatusCode++ .
(++. /
response++/ 7
)++7 8
;++8 9
return,, 
response,, 
.,, 
IsSuccessStatusCode,, /
;,,/ 0
}-- 	
public// 
async// 
Task// 
<// 
bool// 
>// 

IsFollowed//  *
(//* +
string//+ 1
whoUsername//2 =
,//= >
string//? E
whomUsername//F R
)//R S
{00 	
var11 
response11 
=11 
await11  
_client11! (
.11( )
GetAsync11) 1
(111 2
$"112 4 
/fllws/?whoUserName=114 H
{11H I
whoUsername11I T
}11T U
&whomUserName=11U c
{11c d
whomUsername11d p
}11p q
"11q r
)11r s
;11s t
HttpFailureHelper22 
.22 
HandleStatusCode22 .
(22. /
response22/ 7
)227 8
;228 9
var33 
relation33 
=33 
JsonSerializer33 )
.33) *
Deserialize33* 5
<335 6
FollowerDTO336 A
>33A B
(33B C
await33C H
response33I Q
.33Q R
Content33R Y
.33Y Z
ReadAsStringAsync33Z k
(33k l
)33l m
)33m n
;33n o
return44 
relation44 
is44 
not44 "
null44# '
&&44( *
relation44+ 3
.443 4
WhoId444 9
!=44: <
-44= >
$num44> ?
&&44@ B
relation44C K
.44K L
WhomId44L R
!=44S U
-44V W
$num44W X
;44X Y
}55 	
}66 
}77 ª
S/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/Models/HttpFailureHelper.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 
Models #
{ 
public 

static 
class 
HttpFailureHelper )
{ 
public		 
static		 
void		 
HandleStatusCode		 +
(		+ ,
HttpResponseMessage		, ?
response		@ H
)		H I
{

 	
switch 
( 
response 
. 

StatusCode '
)' (
{ 
case 
HttpStatusCode #
.# $
	Forbidden$ -
:- .
throw 
new '
UnauthorizedAccessException 9
(9 :
$str: R
)R S
;S T
case 
HttpStatusCode #
.# $

BadRequest$ .
:. /
throw 
new 
	Exception '
(' (
$"( *1
%You have provided wrong information: * O
{O P
responseP X
.X Y
ReasonPhraseY e
}e f
"f g
)g h
;h i
case 
HttpStatusCode #
.# $
NotFound$ ,
:, -
throw 
new 
	Exception '
(' (
$str( C
)C D
;D E
case 
HttpStatusCode #
.# $
Unauthorized$ 0
:0 1
throw 
new '
UnauthorizedAccessException 9
(9 :
$str: R
)R S
;S T
case 
HttpStatusCode #
.# $
MethodNotAllowed$ 4
:4 5
break 
; 
case 
HttpStatusCode #
.# $
NotAcceptable$ 1
:1 2
break 
; 
case 
HttpStatusCode #
.# $
RequestTimeout$ 2
:2 3
break 
; 
case 
HttpStatusCode #
.# $
Conflict$ ,
:, -
break 
; 
case 
HttpStatusCode #
.# $
InternalServerError$ 7
:7 8
break 
; 
case 
HttpStatusCode #
.# $
NotImplemented$ 2
:2 3
break   
;   
case!! 
HttpStatusCode!! #
.!!# $

BadGateway!!$ .
:!!. /
break"" 
;"" 
case## 
HttpStatusCode## #
.### $
ServiceUnavailable##$ 6
:##6 7
break$$ 
;$$ 
}%% 
}&& 	
}'' 
}(( ©
N/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/Models/MessageModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 
Models #
{ 
public 

class 
MessageModel 
: 
IMessageModel  -
{ 
private 

HttpClient 
_client "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
MessageModel 
( 

HttpClient &
client' -
)- .
{ 	
_client 
= 
client 
; 
} 	
public 
async 
Task 
< 
ICollection %
<% &

MessageDTO& 0
>0 1
>1 2
GetMessages3 >
(> ?
string? E
pathF J
)J K
{ 	
var 
messagesFromApi 
=  !
await" '
_client( /
./ 0
GetStringAsync0 >
(> ?
path? C
)C D
;D E
var 
messages 
= 
JsonSerializer )
.) *
Deserialize* 5
<5 6
List6 :
<: ;

MessageDTO; E
>E F
>F G
(G H
messagesFromApiH W
)W X
;X Y
if 
( 
messages 
is 
null  
)  !
throw 
new 
	Exception #
(# $
$str$ ;
); <
;< =
return 
messages 
; 
} 	
public 
async 
Task 
< 
bool 
> 
PostMessage  +
(+ ,
CreateMessage, 9
message: A
,A B
stringC I
usernameJ R
)R S
{   	
var!! 
json!! 
=!! 
JsonSerializer!! %
.!!% &
	Serialize!!& /
(!!/ 0
message!!0 7
)!!7 8
;!!8 9
var"" 
data"" 
="" 
new"" 
StringContent"" (
(""( )
json"") -
,""- .
Encoding""/ 7
.""7 8
UTF8""8 <
,""< =
$str""> P
)""P Q
;""Q R
var## 
response## 
=## 
await##  
_client##! (
.##( )
	PostAsync##) 2
(##2 3
$"##3 5
/msgs/##5 ;
{##; <
username##< D
}##D E
"##E F
,##F G
data##H L
)##L M
;##M N
HttpFailureHelper$$ 
.$$ 
HandleStatusCode$$ .
($$. /
response$$/ 7
)$$7 8
;$$8 9
return%% 
response%% 
.%% 
IsSuccessStatusCode%% /
;%%/ 0
}&& 	
}'' 
}(( ˘
K/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/Models/UserModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 
Models #
{		 
public

 

class

 
	UserModel

 
:

 

IUserModel

 '
{ 
private 

HttpClient 
Client !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
	UserModel 
( 

HttpClient #
client$ *
)* +
{ 	
Client 
= 
client 
; 
} 	
public 
async 
Task 
< 
bool 
> 
RegisterUser  ,
(, -
CreateUserDTO- :
user; ?
)? @
{ 	
var 
json 
= 
JsonSerializer %
.% &
	Serialize& /
(/ 0
user0 4
)4 5
;5 6
var 
data 
= 
new 
StringContent (
(( )
json) -
,- .
Encoding/ 7
.7 8
UTF88 <
,< =
$str> P
)P Q
;Q R
var 
response 
= 
await  
Client! '
.' (
	PostAsync( 1
(1 2
$"2 4
	/register4 =
"= >
,> ?
data@ D
)D E
;E F
HttpFailureHelper 
. 
HandleStatusCode .
(. /
response/ 7
)7 8
;8 9
return 
response 
. 
IsSuccessStatusCode /
;/ 0
} 	
public 
async 
Task 
< 
bool 
> 
	LoginUser  )
() *
LoginUserDTO* 6
user7 ;
); <
{ 	
var 
json 
= 
JsonSerializer %
.% &
	Serialize& /
(/ 0
user0 4
)4 5
;5 6
var 
data 
= 
new 
StringContent (
(( )
json) -
,- .
Encoding/ 7
.7 8
UTF88 <
,< =
$str> P
)P Q
;Q R
var   
response   
=   
await    
Client  ! '
.  ' (
	PostAsync  ( 1
(  1 2
$"  2 4
/login  4 :
"  : ;
,  ; <
data  = A
)  A B
;  B C
HttpFailureHelper!! 
.!! 
HandleStatusCode!! .
(!!. /
response!!/ 7
)!!7 8
;!!8 9
return"" 
response"" 
."" 
IsSuccessStatusCode"" /
;""/ 0
}## 	
public%% 
async%% 
Task%% 
<%% 
UserDTO%% !
>%%! "
GetLoggedInUser%%# 2
(%%2 3
)%%3 4
{&& 	
var'' 
response'' 
='' 
await''  
Client''! '
.''' (
GetAsync''( 0
(''0 1
$str''1 7
)''7 8
;''8 9
HttpFailureHelper(( 
.(( 
HandleStatusCode(( .
(((. /
response((/ 7
)((7 8
;((8 9
return)) 
JsonSerializer)) !
.))! "
Deserialize))" -
<))- .
UserDTO)). 5
>))5 6
())6 7
await))7 <
response))= E
.))E F
Content))F M
.))M N
ReadAsStringAsync))N _
())_ `
)))` a
)))a b
;))b c
}** 	
}++ 
},, Ì
B/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/Program.cs
	namespace 	
MiniTwitApi
 
. 
Client 
{ 
public 

class 
Program 
{ 
public 
static 
async 
Task  
Main! %
(% &
string& ,
[, -
]- .
args/ 3
)3 4
{ 	
var 
builder 
= "
WebAssemblyHostBuilder 0
.0 1
CreateDefault1 >
(> ?
args? C
)C D
;D E
builder 
. 
RootComponents "
." #
Add# &
<& '
App' *
>* +
(+ ,
$str, 2
)2 3
;3 4
builder 
. 
Services 
. 
	AddScoped &
(& '
sp' )
=>* ,
new- 0

HttpClient1 ;
{< =
BaseAddress> I
=J K
newL O
UriP S
(S T
builderT [
.[ \
HostEnvironment\ k
.k l
BaseAddressl w
)w x
}y z
)z {
;{ |
builder 
. 
Services 
. 
AddTransient )
<) *
IRegisterViewModel* <
,< =
RegisterViewModel> O
>O P
(P Q
)Q R
;R S
builder 
. 
Services 
. 
AddTransient )
<) *
ILoginViewModel* 9
,9 :
LoginViewModel; I
>I J
(J K
)K L
;L M
builder 
. 
Services 
. 
AddTransient )
<) * 
IMyTimelineViewModel* >
,> ?
MyTimelineViewModel@ S
>S T
(T U
)U V
;V W
builder 
. 
Services 
. 
AddTransient )
<) *"
IUserTimelineViewModel* @
,@ A!
UserTimelineViewModelB W
>W X
(X Y
)Y Z
;Z [
builder   
.   
Services   
.   
AddTransient   )
<  ) *
IMessageViewModel  * ;
,  ; <
MessageViewModel  = M
>  M N
(  N O
)  O P
;  P Q
builder## 
.## 
Services## 
.## 
AddTransient## )
<##) *

IUserModel##* 4
,##4 5
	UserModel##6 ?
>##? @
(##@ A
)##A B
;##B C
builder$$ 
.$$ 
Services$$ 
.$$ 
AddTransient$$ )
<$$) *
IMessageModel$$* 7
,$$7 8
MessageModel$$9 E
>$$E F
($$F G
)$$G H
;$$H I
builder%% 
.%% 
Services%% 
.%% 
AddTransient%% )
<%%) *
IFollowModel%%* 6
,%%6 7
FollowModel%%8 C
>%%C D
(%%D E
)%%E F
;%%F G
builder(( 
.(( 
Services(( 
.(( 

AddOptions(( '
(((' (
)((( )
;(() *
builder)) 
.)) 
Services)) 
.))  
AddAuthorizationCore)) 1
())1 2
)))2 3
;))3 4
builder** 
.** 
Services** 
.** 
	AddScoped** &
<**& ''
AuthenticationStateProvider**' B
,**B C-
!CustomAuthenticationStateProvider**D e
>**e f
(**f g
)**g h
;**h i
await,, 
builder,, 
.,, 
Build,, 
(,,  
),,  !
.,,! "
RunAsync,," *
(,,* +
),,+ ,
;,,, -
}-- 	
}.. 
}// ˜
^/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/Abstract/ILoginViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
.' (
Abstract( 0
{ 
public 

	interface 
ILoginViewModel $
:% &

IViewModel' 1
{ 
public 
LoginUserDTO 
User  
{! "
get# &
;& '
set( +
;+ ,
}- .
public		 
UserDTO		 
LoggedInUser		 #
{		$ %
get		& )
;		) *
set		+ .
;		. /
}		0 1
public 
new 
string 
Error 
{  !
get" %
;% &
set' *
;* +
}, -
public 
Task 
	LoginUser 
( 
) 
;  
} 
} ¬
`/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/Abstract/IMessageViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
.' (
Abstract( 0
{ 
public		 

	interface		 
IMessageViewModel		 &
:		' (

IViewModel		) 3
{

 
public 
IAsyncEnumerable 
<  

MessageDTO  *
>* +
RequestMessages, ;
(; <
string< B
pathC G
)G H
;H I
public 
new 
string 
Error 
{  !
get" %
;% &
set' *
;* +
}, -
public 
DateTime 
GenerateDateTime (
(( )
int) ,
date- 1
)1 2
;2 3
} 
} ·
c/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/Abstract/IMyTimelineViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
.' (
Abstract( 0
{ 
public 

	interface  
IMyTimelineViewModel )
:* +

IViewModel, 6
{ 
public		 
CreateMessage		 
Message		 $
{		% &
get		' *
;		* +
set		, /
;		/ 0
}		1 2
public

 
UserDTO

 
LoggedInUser

 #
{

$ %
get

& )
;

) *
set

+ .
;

. /
}

0 1
public 
bool 
IsMessageSent !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Path 
{ 
get  
;  !
set" %
;% &
}' (
public 
new 
string 
Error 
{  !
get" %
;% &
set' *
;* +
}, -
public 
Task 
PostMessage 
(  
)  !
;! "
public 
Task 
< 
UserDTO 
> 
GetLoggedInUser ,
(, -
)- .
;. /
} 
} †	
a/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/Abstract/IRegisterViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
.' (
Abstract( 0
{ 
public 

	interface 
IRegisterViewModel '
:( )

IViewModel* 4
{ 
public 
CreateUserDTO 
User !
{" #
get$ '
;' (
set) ,
;, -
}. /
public		 
string		 
RepeatPassword		 $
{		% &
get		' *
;		* +
set		, /
;		/ 0
}		1 2
public

 
new

 
string

 
Error

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
public 
bool 
IsRegistered  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
Task 
RegisterUser  
(  !
)! "
;" #
} 
} ©
e/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/Abstract/IUserTimelineViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
.' (
Abstract( 0
{ 
public 

	interface "
IUserTimelineViewModel +
:, -

IViewModel. 8
{ 
public 
string 
Username 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
string		 
Path		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public

 
new

 
string

 
Error

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
public 
bool 

IsFollowed 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 
IsUnfollowed  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
bool 
FollowIsDone  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
bool 
UnFollowIsDone "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
Task 
UnfollowUser  
(  !
string! '
whoUsername( 3
)3 4
;4 5
public 
Task 

FollowUser 
( 
string %
whoUsername& 1
)1 2
;2 3
public 
Task 
IsUserFollowed "
(" #
)# $
;$ %
} 
} ¯
Y/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/Abstract/IViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
.' (
Abstract( 0
{ 
public 

	interface 

IViewModel 
{ 
string 
Error 
{ 
get 
; 
set 
;  
}! "
} 
} ∂
T/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/LoginViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
{ 
public		 

class		 
LoginViewModel		 
:		  !
ILoginViewModel		" 1
{

 
public 
LoginUserDTO 
User  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
UserDTO 
LoggedInUser #
{$ %
get& )
;) *
set+ .
;. /
}0 1
private 
readonly 

IUserModel #

_userModel$ .
;. /
public 
string 
Error 
{ 
get !
;! "
set# &
;& '
}( )
public 
LoginViewModel 
( 

IUserModel (
	userModel) 2
)2 3
{ 	

_userModel 
= 
	userModel "
;" #
User 
= 
new 
LoginUserDTO #
(# $
)$ %
;% &
} 	
public 
async 
Task 
	LoginUser #
(# $
)$ %
{ 	
try 
{ 
if 
( 
await 

_userModel $
.$ %
	LoginUser% .
(. /
User/ 3
)3 4
)4 5
LoggedInUser  
=! "
new# &
UserDTO' .
(. /
)/ 0
{ 
Username  
=! "
User# '
.' (
Username( 0
,0 1
Email 
= 
User  $
.$ %
Email% *
}   
;   
}!! 
catch"" 
("" 
	Exception"" 
e"" 
)"" 
{## 
Error$$ 
=$$ 
e$$ 
.$$ 
Message$$ !
;$$! "
}%% 
}&& 	
}'' 
}(( ∞
V/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/MessageViewModel.cs
	namespace		 	
MiniTwitApi		
 
.		 
Client		 
.		 

ViewModels		 '
{

 
public 

class 
MessageViewModel !
:" #
IMessageViewModel$ 5
{ 
private 
readonly 
IMessageModel &
_messageModel' 4
;4 5
public 
string 
Error 
{ 
get !
;! "
set# &
;& '
}( )
public 
MessageViewModel 
(  
IMessageModel  -
messageModel. :
): ;
{ 	
_messageModel 
= 
messageModel (
;( )
} 	
public 
async 
IAsyncEnumerable %
<% &

MessageDTO& 0
>0 1
RequestMessages2 A
(A B
stringB H
pathI M
)M N
{ 	
foreach 
( 
var 
m 
in 
await #
_messageModel$ 1
.1 2
GetMessages2 =
(= >
path> B
)B C
)C D
{ 
yield 
return 
m 
; 
} 
} 	
public 
DateTime 
GenerateDateTime (
(( )
int) ,
date- 1
)1 2
{ 	
return 
DateTimeOffset !
.! "$
FromUnixTimeMilliseconds" :
(: ;
date; ?
)? @
.@ A
DateTimeA I
;I J
}   	
}!! 
}"" ı
Y/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/MyTimelineViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
{		 
public

 

class

 
MyTimelineViewModel

 $
:

% & 
IMyTimelineViewModel

' ;
{ 
public 
CreateMessage 
Message $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
UserDTO 
LoggedInUser #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
string 
Error 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 
IsMessageSent !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Path 
{ 
get  
;  !
set" %
;% &
}' (
private 
readonly 
IMessageModel &
_messageModel' 4
;4 5
private 
readonly 

IUserModel #

_userModel$ .
;. /
public 
MyTimelineViewModel "
(" #
IMessageModel# 0
messageModel1 =
,= >

IUserModel? I
	userModelJ S
)S T
{ 	
Message 
= 
new 
CreateMessage '
(' (
)( )
;) *
_messageModel 
= 
messageModel (
;( )

_userModel 
= 
	userModel "
;" #
} 	
public 
async 
Task 
PostMessage %
(% &
)& '
{ 	
try 
{ 
IsMessageSent   
=   
await    %
_messageModel  & 3
.  3 4
PostMessage  4 ?
(  ? @
Message  @ G
,  G H
LoggedInUser  I U
.  U V
Username  V ^
)  ^ _
;  _ `
}!! 
catch"" 
("" 
	Exception"" 
e"" 
)"" 
{## 
Error$$ 
=$$ 
e$$ 
.$$ 
Message$$ !
;$$! "
}%% 
}&& 	
public(( 
async(( 
Task(( 
<(( 
UserDTO(( !
>((! "
GetLoggedInUser((# 2
(((2 3
)((3 4
{)) 	
return** 
await** 

_userModel** #
.**# $
GetLoggedInUser**$ 3
(**3 4
)**4 5
;**5 6
}++ 	
},, 
}// Ø
W/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/RegisterViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
{		 
public

 

class

 
RegisterViewModel

 "
:

# $
IRegisterViewModel

% 7
{ 
public 
CreateUserDTO 
User !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
Error 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 
IsRegistered  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
RepeatPassword $
{% &
get' *
;* +
set, /
;/ 0
}1 2
private 
readonly 

IUserModel #

_userModel$ .
;. /
public 
RegisterViewModel  
(  !

IUserModel! +
	userModel, 5
)5 6
{ 	

_userModel 
= 
	userModel "
;" #
User 
= 
new 
CreateUserDTO $
($ %
)% &
;& '
} 	
public 
async 
Task 
RegisterUser &
(& '
)' (
{ 	
try 
{ 
ValidatePassword  
(  !
)! "
;" #
IsRegistered   
=   
await   $

_userModel  % /
.  / 0
RegisterUser  0 <
(  < =
User  = A
)  A B
;  B C
}!! 
catch"" 
("" 
	Exception"" 
e"" 
)"" 
{## 
Error$$ 
=$$ 
e$$ 
.$$ 
Message$$ !
;$$! "
}%% 
}&& 	
private(( 
void(( 
ValidatePassword(( %
(((% &
)((& '
{)) 	
if** 
(** 
!** 
User** 
.** 
Password** 
.** 
Equals** %
(**% &
RepeatPassword**& 4
)**4 5
)**5 6
throw++ 
new++ 
	Exception++ #
(++# $
$str++$ <
)++< =
;++= >
},, 	
}-- 
}.. Ø$
[/home/joachim/git/PythonKindergarten/MiniTwitApi/Client/ViewModels/UserTimelineViewModel.cs
	namespace 	
MiniTwitApi
 
. 
Client 
. 

ViewModels '
{ 
public 

class !
UserTimelineViewModel &
:' ("
IUserTimelineViewModel) ?
{		 
public

 
string

 
Username

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
string 
Path 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Error 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 

IsFollowed 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
bool 
IsUnfollowed  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
bool 
FollowIsDone  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
bool 
UnFollowIsDone "
{# $
get% (
;( )
set* -
;- .
}/ 0
private 
readonly 
IFollowModel %
_followModel& 2
;2 3
private 
readonly 

IUserModel #

_userModel$ .
;. /
public !
UserTimelineViewModel $
($ %
IFollowModel% 1
followModel2 =
,= >

IUserModel? I
	userModelJ S
)S T
{ 	
_followModel 
= 
followModel &
;& '

_userModel 
= 
	userModel "
;" #
} 	
public 
async 
Task 
IsUserFollowed (
(( )
)) *
{   	
try!! 
{"" 
var$$ 
user$$ 
=$$ 
await$$  

_userModel$$! +
.$$+ ,
GetLoggedInUser$$, ;
($$; <
)$$< =
;$$= >
var%% 
_isFollowed%% 
=%%  !
await%%# (
_followModel%%) 5
.%%5 6

IsFollowed%%6 @
(%%@ A
user%%A E
.%%E F
Username%%F N
,%%N O
Username%%P X
)%%X Y
;%%Y Z

IsFollowed&& 
=&& 
_isFollowed&& (
;&&( )
IsUnfollowed'' 
='' 
!''  
_isFollowed''  +
;''+ ,
}(( 
catch)) 
()) 
	Exception)) 
e)) 
))) 
{** 
Error++ 
=++ 
e++ 
.++ 
Message++ !
;++! "
},, 
}-- 	
public// 
async// 
Task// 

FollowUser// $
(//$ %
string//% +
whoUsername//, 7
)//7 8
{00 	
try11 
{22 
UnFollowIsDone33 
=33  
false33! &
;33& '
FollowIsDone44 
=44 
await44 $
_followModel44% 1
.441 2

FollowUser442 <
(44< =
whoUsername44= H
,44H I
Username44J R
)44R S
;44S T
}55 
catch66 
(66 
	Exception66 
e66 
)66 
{77 
Error88 
=88 
e88 
.88 
Message88 !
;88! "
}99 
}:: 	
public<< 
async<< 
Task<< 
UnfollowUser<< &
(<<& '
string<<' -
whoUsername<<. 9
)<<9 :
{== 	
try>> 
{?? 
FollowIsDone@@ 
=@@ 
false@@ $
;@@$ %
UnFollowIsDoneAA 
=AA  
awaitAA! &
_followModelAA' 3
.AA3 4
UnfollowUserAA4 @
(AA@ A
whoUsernameAAA L
,AAL M
UsernameAAN V
)AAV W
;AAW X
}BB 
catchCC 
(CC 
	ExceptionCC 
eCC 
)CC 
{DD 
ErrorEE 
=EE 
eEE 
.EE 
MessageEE !
;EE! "
}FF 
}GG 	
}HH 
}II 