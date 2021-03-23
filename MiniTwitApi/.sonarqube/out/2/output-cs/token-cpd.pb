‰
T/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/ClientIpCheckActionFilter.cs
	namespace 	
MiniTwitApi
 
. 
Server 
{		 
public

 

class

 %
ClientIpCheckActionFilter

 *
:

+ ,!
ActionFilterAttribute

- B
{ 
private 
readonly 
ILogger  
_logger! (
;( )
private 
readonly 
string 
	_safelist  )
;) *
public %
ClientIpCheckActionFilter (
(( )
string) /
safelist0 8
,8 9
ILogger: A
loggerB H
)H I
{ 	
	_safelist 
= 
safelist  
;  !
_logger 
= 
logger 
; 
} 	
public 
override 
void 
OnActionExecuting .
(. /"
ActionExecutingContext/ E
contextF M
)M N
{ 	
var 
remoteIp 
= 
context "
." #
HttpContext# .
.. /

Connection/ 9
.9 :
RemoteIpAddress: I
;I J
_logger 
. 
LogDebug 
( 
$str ;
,; <
remoteIp= E
)E F
;F G
var 
ip 
= 
	_safelist 
. 
Split $
($ %
$char% (
)( )
;) *
var 
badIp 
= 
true 
; 
if 
( 
remoteIp 
. 
IsIPv4MappedToIPv6 +
)+ ,
{ 
remoteIp 
= 
remoteIp #
.# $
	MapToIPv4$ -
(- .
). /
;/ 0
} 
foreach!! 
(!! 
var!! 
address!!  
in!!! #
ip!!$ &
)!!& '
{"" 
var## 
testIp## 
=## 
	IPAddress## &
.##& '
Parse##' ,
(##, -
address##- 4
)##4 5
;##5 6
if%% 
(%% 
testIp%% 
.%% 
Equals%% !
(%%! "
remoteIp%%" *
)%%* +
)%%+ ,
{&& 
badIp'' 
='' 
false'' !
;''! "
break(( 
;(( 
})) 
}** 
if,, 
(,, 
badIp,, 
),, 
{-- 
_logger.. 
... 

LogWarning.. "
(.." #
$str..# J
,..J K
remoteIp..L T
)..T U
;..U V
context// 
.// 
Result// 
=//  
new//! $
StatusCodeResult//% 5
(//5 6
StatusCodes//6 A
.//A B
Status403Forbidden//B T
)//T U
;//U V
return00 
;00 
}11 
base33 
.33 
OnActionExecuting33 "
(33" #
context33# *
)33* +
;33+ ,
}44 	
}55 
}66 ˚N
W/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Controllers/FollowController.cs
	namespace		 	
MiniTwitApi		
 
.		 
Server		 
.		 
Controllers		 (
{

 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
FollowController !
:" #
ControllerBase$ 2
{ 
private 
readonly 
IFollowerRepository ,
_followerRepository- @
;@ A
private 
readonly 
IUserRepository (
_userRepository) 8
;8 9
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
private 
readonly "
IActionContextAccessor /
	_accessor0 9
;9 :
public 
FollowController 
(  
IFollowerRepository  3

repository4 >
,> ?
IUserRepository@ O
userRepositoryP ^
,^ _
IConfiguration 
configuration (
,( )"
IActionContextAccessor* @
accessorA I
)I J
{ 	
_userRepository 
= 
userRepository ,
;, -
_followerRepository 
=  !

repository" ,
;, -
_configuration 
= 
configuration *
;* +
	_accessor 
= 
accessor  
;  !
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
ActionResult &
<& '
FollowerDTO' 2
>2 3
>3 4)
GetFollowRelationByWhoAndWhom5 R
(R S
[S T
	FromQueryT ]
]] ^
string_ e
whoUserNamef q
,q r
[s t
	FromQueryt }
]} ~
string	 Ö
whomUserName
Ü í
,
í ì
[
î ï
	FromQuery
ï û
]
û ü
long
† §
latest
• ´
)
´ ¨
{ 	
if   
(   
string   
.   
IsNullOrEmpty   $
(  $ %
whomUserName  % 1
)  1 2
||  3 5
string  6 <
.  < =
IsNullOrEmpty  = J
(  J K
whomUserName  K W
)  W X
)  X Y
return!! 

BadRequest!! !
(!!! "
$str!!" Y
)!!Y Z
;!!Z [
var## 
followRelation## 
=##  
await##! &
_followerRepository##' :
.##: ;
	ReadAsync##; D
(##D E
whoUserName##E P
,##P Q
whomUserName##R ^
)##^ _
;##_ `
var$$ 
followerDto$$ 
=$$ 
new$$ !
FollowerDTO$$" -
($$- .
)$$. /
{%% 
WhoId&& 
=&& 
followRelation&& &
.&&& '
WhoId&&' ,
,&&, -
WhomId'' 
='' 
followRelation'' '
.''' (
WhomId''( .
}(( 
;(( 
if)) 
()) 
latest)) 
>)) 
$num)) 
&&)) 
_configuration)) +
[))+ ,
$str)), 9
]))9 :
.)): ;
Contains)); C
())C D
	_accessor))D M
.))M N
ActionContext))N [
.))[ \
HttpContext))\ g
.))g h

Connection))h r
.))r s
RemoteIpAddress	))s Ç
.
))Ç É
ToString
))É ã
(
))ã å
)
))å ç
)
))ç é
)
))é è
Latest** 
.** 
GetInstance** "
(**" #
)**# $
.**$ %
Update**% +
(**+ ,
latest**, 2
)**2 3
;**3 4
return,, 
Ok,, 
(,, 
followerDto,, !
),,! "
;,," #
}-- 	
[// 	
HttpGet//	 
(// 
$str// #
)//# $
]//$ %
public00 
async00 
Task00 
<00 
ActionResult00 &
<00& '
IList00' ,
<00, -
FollowerDTO00- 8
>008 9
>009 :
>00: ; 
GetFollowsByUsername00< P
(00P Q
string00Q W
username00X `
,00` a
[00b c
	FromQuery00c l
]00l m
long00n r
latest00s y
)00y z
{11 	
if22 
(22 
!22 
await22 
_userRepository22 %
.22% &
UserExistsAsync22& 5
(225 6
username226 >
)22> ?
)22? @
return33 
NotFound33 
(33  
)33  !
;33! "
var66 
follows66 
=66 
await66 
_followerRepository66  3
.663 4
ReadAllAsync664 @
(66@ A
username66A I
)66I J
;66J K
if88 
(88 
latest88 
>88 
$num88 
&&88 
_configuration88 +
[88+ ,
$str88, 9
]889 :
.88: ;
Contains88; C
(88C D
	_accessor88D M
.88M N
ActionContext88N [
.88[ \
HttpContext88\ g
.88g h

Connection88h r
.88r s
RemoteIpAddress	88s Ç
.
88Ç É
ToString
88É ã
(
88ã å
)
88å ç
)
88ç é
)
88é è
Latest99 
.99 
GetInstance99 "
(99" #
)99# $
.99$ %
Update99% +
(99+ ,
latest99, 2
)992 3
;993 4
return;; 
Ok;; 
(;; 
follows;; 
);; 
;;; 
}<< 	
[>> 	
HttpPost>>	 
(>> 
$str>> $
)>>$ %
]>>% &
public?? 
async?? 
Task?? 
<?? 
ActionResult?? &
>??& '!
PostFollowsByUsername??( =
(??= >
string??> D
username??E M
,??M N
[??O P
FromBody??P X
]??X Y
Follow??Z `
follow??a g
,??g h
[??i j
	FromQuery??j s
]??s t
long??u y
latest	??z Ä
)
??Ä Å
{@@ 	
ifAA 
(AA 
!AA 
awaitAA 
_userRepositoryAA %
.AA% &
UserExistsAsyncAA& 5
(AA5 6
usernameAA6 >
)AA> ?
)AA? @
returnBB 
NotFoundBB 
(BB  
)BB  !
;BB! "
ifDD 
(DD 
stringDD 
.DD 
IsNullOrEmptyDD #
(DD# $
followDD$ *
.DD* +
ToFollowDD+ 3
)DD3 4
&&DD5 7
stringDD8 >
.DD> ?
IsNullOrEmptyDD? L
(DDL M
followDDM S
.DDS T

ToUnfollowDDT ^
)DD^ _
)DD_ `
returnEE 

BadRequestEE !
(EE! "
$strEE" U
)EEU V
;EEV W
varHH 

actionUserHH 
=HH 
awaitHH "
_userRepositoryHH# 2
.HH2 3
	ReadAsyncHH3 <
(HH< =
usernameHH= E
)HHE F
;HHF G
varII 

targetUserII 
=II 
awaitII "
_userRepositoryII# 2
.II2 3
	ReadAsyncII3 <
(II< =
stringJJ 
.JJ 
IsNullOrEmptyJJ $
(JJ$ %
followJJ% +
.JJ+ ,
ToFollowJJ, 4
)JJ4 5
?JJ6 7
followJJ8 >
.JJ> ?

ToUnfollowJJ? I
:JJJ K
followJJL R
.JJR S
ToFollowJJS [
)JJ[ \
;JJ\ ]
ifMM 
(MM 
stringMM 
.MM 
IsNullOrEmptyMM #
(MM# $
followMM$ *
.MM* +
ToFollowMM+ 3
)MM3 4
)MM4 5
{NN 
awaitOO 
_followerRepositoryOO )
.OO) *
DeleteAsyncOO* 5
(OO5 6
newOO6 9
FollowerDTOOO: E
{PP 
WhoIdQQ 
=QQ 

actionUserQQ &
.QQ& '
IdQQ' )
,QQ) *
WhomIdRR 
=RR 

targetUserRR '
.RR' (
IdRR( *
,RR* +
}SS 
)SS 
;SS 
}TT 
elseUU 
{VV 
awaitWW 
_followerRepositoryWW )
.WW) *
CreateAsyncWW* 5
(WW5 6
newWW6 9
FollowerDTOWW: E
{XX 
WhoIdYY 
=YY 

actionUserYY &
.YY& '
IdYY' )
,YY) *
WhomIdZZ 
=ZZ 

targetUserZZ '
.ZZ' (
IdZZ( *
,ZZ* +
}[[ 
)[[ 
;[[ 
}\\ 
if^^ 
(^^ 
latest^^ 
>^^ 
$num^^ 
&&^^ 
_configuration^^ +
[^^+ ,
$str^^, 9
]^^9 :
.^^: ;
Contains^^; C
(^^C D
	_accessor^^D M
.^^M N
ActionContext^^N [
.^^[ \
HttpContext^^\ g
.^^g h

Connection^^h r
.^^r s
RemoteIpAddress	^^s Ç
.
^^Ç É
ToString
^^É ã
(
^^ã å
)
^^å ç
)
^^ç é
)
^^é è
Latest__ 
.__ 
GetInstance__ "
(__" #
)__# $
.__$ %
Update__% +
(__+ ,
latest__, 2
)__2 3
;__3 4
returnaa 
	NoContentaa 
(aa 
)aa 
;aa 
}bb 	
}cc 
}dd ÍC
X/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Controllers/MessageController.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Controllers (
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
MessageController "
:# $
ControllerBase% 3
{ 
private 
readonly 
IMessageRepository +
_messagesRepository, ?
;? @
private 
readonly 
IUserRepository (
_userRepository) 8
;8 9
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
private 
readonly "
IActionContextAccessor /
	_accessor0 9
;9 :
public 
MessageController  
(  !
IMessageRepository! 3
messagesRepository4 F
,F G
IUserRepositoryH W
userRepositoryX f
,f g
IConfiguration 
configuration (
,( )"
IActionContextAccessor* @
accessorA I
)I J
{ 	
_messagesRepository 
=  !
messagesRepository" 4
;4 5
_userRepository 
= 
userRepository ,
;, -
_configuration 
= 
configuration *
;* +
	_accessor 
= 
accessor  
;  !
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public   
async   
Task   
<   
ActionResult   &
<  & '
IEnumerable  ' 2
<  2 3

MessageDTO  3 =
>  = >
>  > ?
>  ? @
GetMsgs  A H
(  H I
[  I J
	FromQuery  J S
]  S T
int  U X
no  Y [
,  [ \
[  ] ^
	FromQuery  ^ g
]  g h
int  i l
skip  m q
,  q r
[  s t
	FromQuery  t }
]  } ~
long	   É
latest
  Ñ ä
)
  ä ã
{!! 	
var## 
messages## 
=## 
await##  
_messagesRepository##! 4
.##4 5
ReadAllAsync##5 A
(##A B
no##B D
,##D E
skip##F J
)##J K
;##K L
if%% 
(%% 
latest%% 
>%% 
$num%% 
&&%% 
_configuration%% +
[%%+ ,
$str%%, 9
]%%9 :
.%%: ;
Contains%%; C
(%%C D
	_accessor%%D M
.%%M N
ActionContext%%N [
.%%[ \
HttpContext%%\ g
.%%g h

Connection%%h r
.%%r s
RemoteIpAddress	%%s Ç
.
%%Ç É
ToString
%%É ã
(
%%ã å
)
%%å ç
)
%%ç é
)
%%é è
Latest&& 
.&& 
GetInstance&& "
(&&" #
)&&# $
.&&$ %
Update&&% +
(&&+ ,
latest&&, 2
)&&2 3
;&&3 4
return)) 
Ok)) 
()) 
messages)) 
))) 
;))  
}** 	
[,, 	
HttpGet,,	 
(,, 
$str,, "
),," #
],,# $
public-- 
async-- 
Task-- 
<-- 
ActionResult-- &
<--& '
IEnumerable--' 2
<--2 3

MessageDTO--3 =
>--= >
>--> ?
>--? @
GetMsgsByUsername--A R
(--R S
string--S Y
username--Z b
,--b c
[--d e
	FromQuery--e n
]--n o
int--p s
no--t v
,--v w
[--x y
	FromQuery	--y Ç
]
--Ç É
int
--Ñ á
skip
--à å
,
--å ç
[
--é è
	FromQuery
--è ò
]
--ò ô
long
--ö û
latest
--ü •
)
--• ¶
{.. 	
if// 
(// 
!// 
await// 
_userRepository// %
.//% &
UserExistsAsync//& 5
(//5 6
username//6 >
)//> ?
)//? @
return00 
NotFound00 
(00  
)00  !
;00! "
var33 
messages33 
=33 
await33  
_messagesRepository33! 4
.334 5
ReadAllUserAsync335 E
(33E F
username33F N
,33N O
no33P R
,33R S
skip33T X
)33X Y
;33Y Z
if55 
(55 
latest55 
>55 
$num55 
&&55 
_configuration55 +
[55+ ,
$str55, 9
]559 :
.55: ;
Contains55; C
(55C D
	_accessor55D M
.55M N
ActionContext55N [
.55[ \
HttpContext55\ g
.55g h

Connection55h r
.55r s
RemoteIpAddress	55s Ç
.
55Ç É
ToString
55É ã
(
55ã å
)
55å ç
)
55ç é
)
55é è
Latest66 
.66 
GetInstance66 "
(66" #
)66# $
.66$ %
Update66% +
(66+ ,
latest66, 2
)662 3
;663 4
return99 
Ok99 
(99 
messages99 
)99 
;99  
}:: 	
[<< 	
HttpPost<<	 
(<< 
$str<< #
)<<# $
]<<$ %
public== 
async== 
Task== 
<== 
ActionResult== &
>==& '!
PostMessageByUsername==( =
(=== >
string==> D
username==E M
,==M N
[==O P
FromBody==P X
]==X Y
CreateMessage==Z g
createMessage==h u
,==u v
[==w x
	FromQuery	==x Å
]
==Å Ç
int
==É Ü
latest
==á ç
)
==ç é
{>> 	
if@@ 
(@@ 
!@@ 
await@@ 
_userRepository@@ %
.@@% &
UserExistsAsync@@& 5
(@@5 6
username@@6 >
)@@> ?
)@@? @
returnAA 
NotFoundAA 
(AA  
)AA  !
;AA! "
ifCC 
(CC 
stringCC 
.CC 
IsNullOrEmptyCC #
(CC# $
createMessageCC$ 1
.CC1 2
ContentCC2 9
)CC9 :
)CC: ;
returnDD 

BadRequestDD !
(DD! "
$strDD" =
)DD= >
;DD> ?
varHH 

actionUserHH 
=HH 
awaitHH "
_userRepositoryHH# 2
.HH2 3
	ReadAsyncHH3 <
(HH< =
usernameHH= E
)HHE F
;HHF G
awaitII 
_messagesRepositoryII %
.II% &
CreateAsyncII& 1
(II1 2
newII2 5

MessageDTOII6 @
{JJ 
AuthorKK 
=KK 

actionUserKK #
.KK# $
IdKK$ &
,KK& '
AuthorUsernameLL 
=LL  
usernameLL! )
,LL) *
TextMM 
=MM 
createMessageMM $
.MM$ %
ContentMM% ,
,MM, -
PublishDateNN 
=NN 
(NN 
intNN "
)NN" #
EpochConverterNN$ 2
.NN2 3
ToEpochNN3 :
(NN: ;
DateTimeNN; C
.NNC D
NowNND G
)NNG H
,NNH I
FlaggedOO 
=OO 
$numOO 
}PP 
)PP 
;PP 
ifRR 
(RR 
latestRR 
>RR 
$numRR 
&&RR 
_configurationRR +
[RR+ ,
$strRR, 9
]RR9 :
.RR: ;
ContainsRR; C
(RRC D
	_accessorRRD M
.RRM N
ActionContextRRN [
.RR[ \
HttpContextRR\ g
.RRg h

ConnectionRRh r
.RRr s
RemoteIpAddress	RRs Ç
.
RRÇ É
ToString
RRÉ ã
(
RRã å
)
RRå ç
)
RRç é
)
RRé è
LatestSS 
.SS 
GetInstanceSS "
(SS" #
)SS# $
.SS$ %
UpdateSS% +
(SS+ ,
latestSS, 2
)SS2 3
;SS3 4
returnUU 
	NoContentUU 
(UU 
)UU 
;UU 
}VV 	
}WW 
}XX ∆
U/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Controllers/MiscController.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Controllers (
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
MiscController 
:  !
ControllerBase" 0
{ 
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
private 
readonly "
IActionContextAccessor /
	_accessor0 9
;9 :
public 
MiscController 
( 
IConfiguration ,
configuration- :
,: ;"
IActionContextAccessor< R
accessorS [
)[ \
{ 	
_configuration 
= 
configuration *
;* +
	_accessor 
= 
accessor  
;  !
} 	
[ 	
HttpGet	 
( 
$str 
) 
] 
public 
async 
Task 
< 
ActionResult &
<& '
GetLatestResponse' 8
>8 9
>9 :
	GetLatest; D
(D E
[E F
	FromQueryF O
]O P
longQ U
latestV \
)\ ]
{ 	
if 
( 
latest 
> 
$num 
&& 
_configuration +
[+ ,
$str, 9
]9 :
.: ;
Contains; C
(C D
	_accessorD M
.M N
ActionContextN [
.[ \
HttpContext\ g
.g h

Connectionh r
.r s
RemoteIpAddress	s Ç
.
Ç É
ToString
É ã
(
ã å
)
å ç
)
ç é
)
é è
Latest 
. 
GetInstance "
(" #
)# $
.$ %
Update% +
(+ ,
latest, 2
)2 3
;3 4
return 
Ok 
( 
new 
GetLatestResponse +
(+ ,
Latest, 2
.2 3
GetInstance3 >
(> ?
)? @
.@ A
ReadA E
(E F
)F G
)G H
)H I
;I J
}   	
}$$ 
}%% •a
U/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Controllers/UserController.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Controllers (
{ 
[ 
ApiController 
] 
[ 
Route 

(
 
$str 
) 
] 
public 

class 
UserController 
:  !
ControllerBase" 0
{ 
private 
readonly 
IUserRepository (
_repository) 4
;4 5
private 
readonly 
IConfiguration '
_configuration( 6
;6 7
private 
readonly "
IActionContextAccessor /
	_accessor0 9
;9 :
public   
UserController   
(   
IUserRepository   -

repository  . 8
,  8 9
IConfiguration  : H
configuration  I V
,  V W"
IActionContextAccessor  X n
accessor  o w
)  w x
{!! 	
_repository"" 
="" 

repository"" $
;""$ %
_configuration## 
=## 
configuration## *
;##* +
	_accessor$$ 
=$$ 
accessor$$  
;$$  !
}%% 	
['' 	
HttpPost''	 
('' 
$str'' 
)'' 
]'' 
public(( 
async(( 
Task(( 
<(( 
ActionResult(( &
>((& '
GetLogin((( 0
(((0 1
[((1 2
FromBody((2 :
]((: ;
LoginUserDTO((< H
user((I M
,((M N
[((O P
	FromQuery((P Y
]((Y Z
long(([ _
latest((` f
)((f g
{)) 	
var** 
userFromDatabase**  
=**! "
await**# (
_repository**) 4
.**4 5
	ReadAsync**5 >
(**> ?
user**? C
.**C D
Username**D L
)**L M
;**M N
if,, 
(,, 
userFromDatabase,, 
is,,  "
null,,# '
),,' (
return-- 

BadRequest-- !
(--! "
$str--" 7
)--7 8
;--8 9
if// 
(// 
!// 
BCrypt// 
.// 
CheckPassword// $
(//$ %
user//% )
.//) *
Password//* 2
,//2 3
userFromDatabase//4 D
.//D E
Password//E M
)//M N
)//N O
return00 

BadRequest00 !
(00! "
$str00" >
)00> ?
;00? @
var22 
claims22 
=22 
new22 
List22 !
<22! "
Claim22" '
>22' (
{33 
new44 
(44 

ClaimTypes44 
.44  
Name44  $
,44$ %
userFromDatabase44& 6
.446 7
Username447 ?
)44? @
,44@ A
new55 
(55 

ClaimTypes55 
.55  
Email55  %
,55% &
userFromDatabase55' 7
.557 8
Email558 =
)55= >
}66 
;66 
var88 
claimsIdentity88 
=88  
new88! $
ClaimsIdentity88% 3
(883 4
claims884 :
,88: ;(
CookieAuthenticationDefaults88< X
.88X Y 
AuthenticationScheme88Y m
)88m n
;88n o
var:: 
authProperties:: 
=::  
new::! $$
AuthenticationProperties::% =
(::= >
)::> ?
;::? @
await<< 
HttpContext<< 
.<< 
SignInAsync<< )
(<<) *(
CookieAuthenticationDefaults== ,
.==, - 
AuthenticationScheme==- A
,==A B
new>> 
ClaimsPrincipal>> #
(>># $
claimsIdentity>>$ 2
)>>2 3
,>>3 4
authProperties?? 
)?? 
;??  
ifAA 
(AA 
latestAA 
>AA 
$numAA 
&&AA 
_configurationAA +
[AA+ ,
$strAA, 9
]AA9 :
.AA: ;
ContainsAA; C
(AAC D
	_accessorAAD M
.AAM N
ActionContextAAN [
.AA[ \
HttpContextAA\ g
.AAg h

ConnectionAAh r
.AAr s
RemoteIpAddress	AAs Ç
.
AAÇ É
ToString
AAÉ ã
(
AAã å
)
AAå ç
)
AAç é
)
AAé è
LatestBB 
.BB 
GetInstanceBB "
(BB" #
)BB# $
.BB$ %
UpdateBB% +
(BB+ ,
latestBB, 2
)BB2 3
;BB3 4
returnDD 
	NoContentDD 
(DD 
)DD 
;DD 
}EE 	
[GG 	
HttpGetGG	 
(GG 
$strGG 
)GG 
]GG 
publicHH 
asyncHH 
TaskHH 
<HH 
ActionResultHH &
>HH& '
	GetLogoutHH( 1
(HH1 2
)HH2 3
{II 	
awaitJJ 
HttpContextJJ 
.JJ 
SignOutAsyncJJ *
(JJ* +(
CookieAuthenticationDefaultsJJ+ G
.JJG H 
AuthenticationSchemeJJH \
)JJ\ ]
;JJ] ^
returnKK 
	NoContentKK 
(KK 
)KK 
;KK 
}LL 	
[NN 	
HttpGetNN	 
(NN 
$strNN 
)NN 
]NN 
publicOO 
asyncOO 
TaskOO 
<OO 
ActionResultOO &
>OO& '
GetLoggedInUserOO( 7
(OO7 8
)OO8 9
{PP 	
ifQQ 
(QQ 
HttpContextQQ 
.QQ 
UserQQ  
.QQ  !
IdentityQQ! )
isQQ* ,
nullQQ- 1
||QQ2 4
!QQ5 6
HttpContextQQ6 A
.QQA B
UserQQB F
.QQF G
IdentityQQG O
.QQO P
IsAuthenticatedQQP _
)QQ_ `
returnRR 
newRR 
StatusCodeResultRR +
(RR+ ,
(RR, -
intRR- 0
)RR0 1
HttpStatusCodeRR1 ?
.RR? @
	ForbiddenRR@ I
)RRI J
;RRJ K
varTT 
usernameTT 
=TT 
HttpContextTT &
.TT& '
UserTT' +
.TT+ ,
IdentityTT, 4
.TT4 5
NameTT5 9
;TT9 :
varUU 
emailUU 
=UU 
HttpContextUU #
.UU# $
UserUU$ (
.UU( )
ClaimsUU) /
.UU/ 0
WhereVV !
(VV! "
cVV" #
=>VV$ &
cVV' (
.VV( )
TypeVV) -
.VV- .
EqualsVV. 4
(VV4 5

ClaimTypesVV5 ?
.VV? @
EmailVV@ E
)VVE F
)VVF G
.WW 
SelectWW #
(WW# $
cWW$ %
=>WW& (
cWW) *
)WW* +
.XX 
FirstXX "
(XX" #
)XX# $
.XX$ %
ValueXX% *
;XX* +
varYY 
userYY 
=YY 
newYY 
UserDTOYY "
(YY" #
)YY# $
{ZZ 
Username[[ 
=[[ 
username[[ #
,[[# $
Email\\ 
=\\ 
email\\ 
}]] 
;]] 
return^^ 
Ok^^ 
(^^ 
user^^ 
)^^ 
;^^ 
}__ 	
[aa 	
HttpPostaa	 
(aa 
$straa 
)aa 
]aa 
publicbb 
asyncbb 
Taskbb 
<bb 
ActionResultbb &
>bb& '
PostRegisterbb( 4
(bb4 5
[bb5 6
FromBodybb6 >
]bb> ?
CreateUserDTObb@ M
userbbN R
,bbR S
[bbT U
	FromQuerybbU ^
]bb^ _
longbb` d
latestbbe k
)bbk l
{cc 	
ifee 
(ee 
stringee 
.ee 
IsNullOrEmptyee #
(ee# $
useree$ (
.ee( )
Usernameee) 1
)ee1 2
)ee2 3
returnff 

BadRequestff !
(ff! "
$strff" @
)ff@ A
;ffA B
ifii 
(ii 
stringii 
.ii 
IsNullOrEmptyii #
(ii# $
userii$ (
.ii( )
Passwordii) 1
)ii1 2
)ii2 3
returnjj 

BadRequestjj !
(jj! "
$strjj" @
)jj@ A
;jjA B
ifmm 
(mm 
stringmm 
.mm 
IsNullOrEmptymm #
(mm# $
usermm$ (
.mm( )
Emailmm) .
)mm. /
)mm/ 0
returnnn 

BadRequestnn !
(nn! "
$strnn" K
)nnK L
;nnL M
ifqq 
(qq 
awaitqq 
_repositoryqq  
.qq  !
UserExistsAsyncqq! 0
(qq0 1
userqq1 5
.qq5 6
Usernameqq6 >
)qq> ?
)qq? @
returnrr 

BadRequestrr !
(rr! "
$strrr" K
)rrK L
;rrL M
varuu 
hashedPassworduu 
=uu  
BCryptuu! '
.uu' (
HashPassworduu( 4
(uu4 5
useruu5 9
.uu9 :
Passworduu: B
,uuB C
BCryptuuD J
.uuJ K
GenerateSaltuuK W
(uuW X
$numuuX Z
)uuZ [
)uu[ \
;uu\ ]
uservv 
.vv 
Passwordvv 
=vv 
hashedPasswordvv *
;vv* +
awaitww 
_repositoryww 
.ww 
CreateAsyncww )
(ww) *
userww* .
)ww. /
;ww/ 0
ifyy 
(yy 
latestyy 
>yy 
$numyy 
&&yy 
_configurationyy +
[yy+ ,
$stryy, 9
]yy9 :
.yy: ;
Containsyy; C
(yyC D
	_accessoryyD M
.yyM N
ActionContextyyN [
.yy[ \
HttpContextyy\ g
.yyg h

Connectionyyh r
.yyr s
RemoteIpAddress	yys Ç
.
yyÇ É
ToString
yyÉ ã
(
yyã å
)
yyå ç
)
yyç é
)
yyé è
Latestzz 
.zz 
GetInstancezz "
(zz" #
)zz# $
.zz$ %
Updatezz% +
(zz+ ,
latestzz, 2
)zz2 3
;zz3 4
return|| 
	NoContent|| 
(|| 
)|| 
;|| 
}}} 	
[ 	
HttpGet	 
( 
$str  
)  !
]! "
public
ÄÄ 
async
ÄÄ 
Task
ÄÄ 
<
ÄÄ 
ActionResult
ÄÄ &
<
ÄÄ& '
UserDTO
ÄÄ' .
>
ÄÄ. /
>
ÄÄ/ 0
GetUserByUserId
ÄÄ1 @
(
ÄÄ@ A
int
ÄÄA D
userid
ÄÄE K
,
ÄÄK L
[
ÄÄM N
	FromQuery
ÄÄN W
]
ÄÄW X
long
ÄÄY ]
latest
ÄÄ^ d
)
ÄÄd e
{
ÅÅ 	
var
ÇÇ 
user
ÇÇ 
=
ÇÇ 
await
ÇÇ 
_repository
ÇÇ (
.
ÇÇ( )
	ReadAsync
ÇÇ) 2
(
ÇÇ2 3
userid
ÇÇ3 9
)
ÇÇ9 :
;
ÇÇ: ;
if
ÑÑ 
(
ÑÑ 
latest
ÑÑ 
>
ÑÑ 
$num
ÑÑ 
&&
ÑÑ 
_configuration
ÑÑ +
[
ÑÑ+ ,
$str
ÑÑ, 9
]
ÑÑ9 :
.
ÑÑ: ;
Contains
ÑÑ; C
(
ÑÑC D
	_accessor
ÑÑD M
.
ÑÑM N
ActionContext
ÑÑN [
.
ÑÑ[ \
HttpContext
ÑÑ\ g
.
ÑÑg h

Connection
ÑÑh r
.
ÑÑr s
RemoteIpAddressÑÑs Ç
.ÑÑÇ É
ToStringÑÑÉ ã
(ÑÑã å
)ÑÑå ç
)ÑÑç é
)ÑÑé è
Latest
ÖÖ 
.
ÖÖ 
GetInstance
ÖÖ "
(
ÖÖ" #
)
ÖÖ# $
.
ÖÖ$ %
Update
ÖÖ% +
(
ÖÖ+ ,
latest
ÖÖ, 2
)
ÖÖ2 3
;
ÖÖ3 4
return
áá 
Ok
áá 
(
áá 
user
áá 
)
áá 
;
áá 
}
àà 	
}
ââ 
}ää ç 
K/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Entities/Context.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Entities %
{ 
public 

class 
Context 
: 
	DbContext $
,$ %
IContext& .
{ 
public 
DbSet 
< 
User 
> 
Users  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
DbSet 
< 
Follower 
> 
	Followers (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public		 
DbSet		 
<		 
Message		 
>		 
Messages		 &
{		' (
get		) ,
;		, -
set		. 1
;		1 2
}		3 4
public 
Context 
( 
) 
{ 	
}	 

public 
Context 
( 
DbContextOptions '
<' (
Context( /
>/ 0
contextOptions1 ?
)? @
:A B
baseC G
(G H
contextOptionsH V
)V W
{ 	
}	 

	protected 
override 
void 
OnConfiguring  -
(- .#
DbContextOptionsBuilder. E
optionsBuilderF T
)T U
{ 	
if 
( 
! 
optionsBuilder 
.  
IsConfigured  ,
), -
{ 
optionsBuilder 
. 
	UseNpgsql (
(( )
$str	) â
)
â ä
;
ä ã
} 
} 	
	protected 
override 
void 
OnModelCreating  /
(/ 0
ModelBuilder0 <
modelBuilder= I
)I J
{ 	
modelBuilder 
. 
Entity 
<  
Message  '
>' (
(( )
)) *
. 
HasOne 
< 
User 
> 
( 
m 
=>  "
m# $
.$ %
User% )
)) *
.   
WithMany   
(   
)   
.!! 
HasForeignKey!! 
(!! 
m!!  
=>!!! #
m!!$ %
.!!% &
AuthorId!!& .
)!!. /
;!!/ 0
modelBuilder$$ 
.$$ 
Entity$$ 
<$$  
Follower$$  (
>$$( )
($$) *
)$$* +
.$$+ ,
HasKey$$, 2
($$2 3
f$$3 4
=>$$5 7
new$$8 ;
{$$< =
f$$= >
.$$> ?
WhoId$$? D
,$$D E
f$$F G
.$$G H
WhomId$$H N
}$$N O
)$$O P
;$$P Q
modelBuilder'' 
.'' 
Entity'' 
<''  
User''  $
>''$ %
(''% &
)''& '
.(( 
HasIndex(( 
((( 
u(( 
=>(( 
u((  
.((  !
Username((! )
)(() *
.)) 
IsUnique)) 
()) 
))) 
;)) 
modelBuilder,, 
.,, 
Entity,, 
<,,  
Follower,,  (
>,,( )
(,,) *
),,* +
.-- 
HasOne-- 
<-- 
User-- 
>-- 
(-- 
u-- 
=>--  "
u--# $
.--$ %
Who--% (
)--( )
... 
WithMany.. 
(.. 
u.. 
=>.. 
u..  
...  !
	Followers..! *
)..* +
.// 
OnDelete// 
(// 
DeleteBehavior// (
.//( )
ClientCascade//) 6
)//6 7
;//7 8
}00 	
}11 
}22 Ô
L/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Entities/Follower.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Entities %
{ 
public 

class 
Follower 
{ 
[ 	
Key	 
] 
public 
int 
WhoId 
{ 
get 
; 
set !
;! "
}" #
public

 
User

 
Who

 
{

 
get

 
;

 
set

 "
;

" #
}

$ %
[ 	
Key	 
] 
public 
int 
WhomId 
{ 
get 
; 
set "
;" #
}# $
public 
User 
Whom 
{ 
get 
; 
set  #
;# $
}% &
} 
} å	
L/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Entities/IContext.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Entities %
{ 
public 

	interface 
IContext 
{ 
public		 
DbSet		 
<		 
Follower		 
>		 
	Followers		 (
{		) *
get		+ .
;		. /
set		0 3
;		3 4
}		5 6
public

 
DbSet

 
<

 
User

 
>

 
Users

  
{

! "
get

# &
;

& '
set

( +
;

+ ,
}

- .
public 
DbSet 
< 
Message 
> 
Messages &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
Task 
< 
int 
> 
SaveChangesAsync "
(" #
CancellationToken# 4
cancellation5 A
=B C
defaultD K
)K L
;L M
} 
} È
K/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Entities/Message.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Entities %
{ 
public 

class 
Message 
{ 
[
 
Key 
] 
public
 
int 
	MessageId 
{  
get  #
;# $
set% (
;( )
}* +
[		
 
Required		 
]		 
public


 
int

 
AuthorId

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
[
 
Required 
] 
public
 
User 
User 
{ 
get 
;  
set! $
;$ %
}% &
public
 
string 
AuthorUsername &
{' (
get( +
;+ ,
set- 0
;0 1
}2 3
[
 
Required 
] 
public
 
string 
Text 
{ 
get !
;! "
set# &
;& '
}( )
[
 
Required 
] 
public
 
int 
PubDate 
{ 
get !
;! "
set# &
;& '
}( )
public
 
int 
Flagged 
{ 
get !
;! "
set# &
;& '
}( )
} 
} é
H/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Entities/User.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Entities %
{ 
public 

class 
User 
{ 
[ 	
Key	 
] 
public		 
int		 
UserId		 
{		 
get		 
;		 
set		  #
;		# $
}		$ %
[ 	
Required	 
] 
public 
string 
Username 
{  
get  #
;# $
set% (
;( )
}) *
[ 	
Required	 
] 
public 
string 
Email 
{ 
get 
;  
set! $
;$ %
}% &
[ 	
Required	 
] 
public 
string 
Password 
{ 
get "
;" #
set$ '
;' (
}( )
public 
ICollection 
< 
Follower #
># $
	Followers% .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 
ICollection 
< 
Message "
>" #
Messages$ ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
} 
} ≠
A/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Latest.cs
	namespace 	
MiniTwitApi
 
. 
Server 
{ 
public 

class 
Latest 
{ 
private 
static 
readonly 
Latest  &
_latest' .
=/ 0
new1 4
Latest5 ;
(; <
)< =
;= >
private 
readonly 
object 
	_fileLock  )
=* +
new, /
object0 6
(6 7
)7 8
;8 9
public

 
void

 
Update

 
(

 
long

 
latest

  &
)

& '
{ 	
lock 
( 
	_fileLock 
) 
{ 
using 
var 
writer  
=! "
new# &
StreamWriter' 3
(3 4
$str4 @
)@ A
;A B
writer 
. 
	WriteLine  
(  !
$"! #
{# $
latest$ *
}* +
"+ ,
), -
;- .
writer 
. 
Close 
( 
) 
; 
} 
} 	
public 
long 
Read 
( 
) 
{ 	
lock 
( 
	_fileLock 
) 
{ 
using 
var 
reader  
=! "
new# &
StreamReader' 3
(3 4
$str4 @
)@ A
;A B
var 
latest 
= 
long !
.! "
Parse" '
(' (
reader( .
.. /
ReadLine/ 7
(7 8
)8 9
)9 :
;: ;
reader 
. 
Close 
( 
) 
; 
return 
latest 
; 
} 
} 	
public 
static 
Latest 
GetInstance (
(( )
)) *
{   	
return!! 
_latest!! 
;!! 
}"" 	
}## 
}$$ ‹U
]/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Migrations/20210303175047_intitial.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 

Migrations '
{ 
public 

partial 
class 
intitial !
:" #
	Migration$ -
{ 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder		 
.		 
CreateTable		 (
(		( )
name

 
:

 
$str

 
,

 
columns 
: 
table 
=> !
new" %
{ 
UserId 
= 
table "
." #
Column# )
<) *
int* -
>- .
(. /
type/ 3
:3 4
$str5 >
,> ?
nullable@ H
:H I
falseJ O
)O P
. 

Annotation #
(# $
$str$ :
,: ;
true< @
)@ A
,A B
Username 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
type4 8
:8 9
$str: @
,@ A
nullableB J
:J K
falseL Q
)Q R
,R S
Email 
= 
table !
.! "
Column" (
<( )
string) /
>/ 0
(0 1
type1 5
:5 6
$str7 =
,= >
nullable? G
:G H
falseI N
)N O
,O P
Password 
= 
table $
.$ %
Column% +
<+ ,
string, 2
>2 3
(3 4
type4 8
:8 9
$str: @
,@ A
nullableB J
:J K
falseL Q
)Q R
} 
, 
constraints 
: 
table "
=># %
{ 
table 
. 

PrimaryKey $
($ %
$str% /
,/ 0
x1 2
=>3 5
x6 7
.7 8
UserId8 >
)> ?
;? @
} 
) 
; 
migrationBuilder 
. 
CreateTable (
(( )
name 
: 
$str !
,! "
columns 
: 
table 
=> !
new" %
{ 
WhoId 
= 
table !
.! "
Column" (
<( )
int) ,
>, -
(- .
type. 2
:2 3
$str4 =
,= >
nullable? G
:G H
falseI N
)N O
,O P
WhomId 
= 
table "
." #
Column# )
<) *
int* -
>- .
(. /
type/ 3
:3 4
$str5 >
,> ?
nullable@ H
:H I
falseJ O
)O P
} 
, 
constraints 
: 
table "
=># %
{   
table!! 
.!! 

PrimaryKey!! $
(!!$ %
$str!!% 3
,!!3 4
x!!5 6
=>!!7 9
new!!: =
{!!> ?
x!!@ A
.!!A B
WhoId!!B G
,!!G H
x!!I J
.!!J K
WhomId!!K Q
}!!R S
)!!S T
;!!T U
table"" 
."" 

ForeignKey"" $
(""$ %
name## 
:## 
$str## 8
,##8 9
column$$ 
:$$ 
x$$  !
=>$$" $
x$$% &
.$$& '
WhoId$$' ,
,$$, -
principalTable%% &
:%%& '
$str%%( /
,%%/ 0
principalColumn&& '
:&&' (
$str&&) 1
,&&1 2
onDelete''  
:''  !
ReferentialAction''" 3
.''3 4
Restrict''4 <
)''< =
;''= >
table(( 
.(( 

ForeignKey(( $
((($ %
name)) 
:)) 
$str)) 9
,))9 :
column** 
:** 
x**  !
=>**" $
x**% &
.**& '
WhomId**' -
,**- .
principalTable++ &
:++& '
$str++( /
,++/ 0
principalColumn,, '
:,,' (
$str,,) 1
,,,1 2
onDelete--  
:--  !
ReferentialAction--" 3
.--3 4
Cascade--4 ;
)--; <
;--< =
}.. 
).. 
;.. 
migrationBuilder00 
.00 
CreateTable00 (
(00( )
name11 
:11 
$str11  
,11  !
columns22 
:22 
table22 
=>22 !
new22" %
{33 
	MessageId44 
=44 
table44  %
.44% &
Column44& ,
<44, -
int44- 0
>440 1
(441 2
type442 6
:446 7
$str448 A
,44A B
nullable44C K
:44K L
false44M R
)44R S
.55 

Annotation55 #
(55# $
$str55$ :
,55: ;
true55< @
)55@ A
,55A B
AuthorId66 
=66 
table66 $
.66$ %
Column66% +
<66+ ,
int66, /
>66/ 0
(660 1
type661 5
:665 6
$str667 @
,66@ A
nullable66B J
:66J K
false66L Q
)66Q R
,66R S
AuthorUsername77 "
=77# $
table77% *
.77* +
Column77+ 1
<771 2
string772 8
>778 9
(779 :
type77: >
:77> ?
$str77@ F
,77F G
nullable77H P
:77P Q
true77R V
)77V W
,77W X
Text88 
=88 
table88  
.88  !
Column88! '
<88' (
string88( .
>88. /
(88/ 0
type880 4
:884 5
$str886 <
,88< =
nullable88> F
:88F G
false88H M
)88M N
,88N O
PubDate99 
=99 
table99 #
.99# $
Column99$ *
<99* +
int99+ .
>99. /
(99/ 0
type990 4
:994 5
$str996 ?
,99? @
nullable99A I
:99I J
false99K P
)99P Q
,99Q R
Flagged:: 
=:: 
table:: #
.::# $
Column::$ *
<::* +
int::+ .
>::. /
(::/ 0
type::0 4
:::4 5
$str::6 ?
,::? @
nullable::A I
:::I J
false::K P
)::P Q
,::Q R
UserId1;; 
=;; 
table;; #
.;;# $
Column;;$ *
<;;* +
int;;+ .
>;;. /
(;;/ 0
type;;0 4
:;;4 5
$str;;6 ?
,;;? @
nullable;;A I
:;;I J
true;;K O
);;O P
}<< 
,<< 
constraints== 
:== 
table== "
=>==# %
{>> 
table?? 
.?? 

PrimaryKey?? $
(??$ %
$str??% 2
,??2 3
x??4 5
=>??6 8
x??9 :
.??: ;
	MessageId??; D
)??D E
;??E F
table@@ 
.@@ 

ForeignKey@@ $
(@@$ %
nameAA 
:AA 
$strAA :
,AA: ;
columnBB 
:BB 
xBB  !
=>BB" $
xBB% &
.BB& '
AuthorIdBB' /
,BB/ 0
principalTableCC &
:CC& '
$strCC( /
,CC/ 0
principalColumnDD '
:DD' (
$strDD) 1
,DD1 2
onDeleteEE  
:EE  !
ReferentialActionEE" 3
.EE3 4
CascadeEE4 ;
)EE; <
;EE< =
tableFF 
.FF 

ForeignKeyFF $
(FF$ %
nameGG 
:GG 
$strGG 9
,GG9 :
columnHH 
:HH 
xHH  !
=>HH" $
xHH% &
.HH& '
UserId1HH' .
,HH. /
principalTableII &
:II& '
$strII( /
,II/ 0
principalColumnJJ '
:JJ' (
$strJJ) 1
,JJ1 2
onDeleteKK  
:KK  !
ReferentialActionKK" 3
.KK3 4
RestrictKK4 <
)KK< =
;KK= >
}LL 
)LL 
;LL 
migrationBuilderNN 
.NN 
CreateIndexNN (
(NN( )
nameOO 
:OO 
$strOO +
,OO+ ,
tablePP 
:PP 
$strPP "
,PP" #
columnQQ 
:QQ 
$strQQ  
)QQ  !
;QQ! "
migrationBuilderSS 
.SS 
CreateIndexSS (
(SS( )
nameTT 
:TT 
$strTT ,
,TT, -
tableUU 
:UU 
$strUU !
,UU! "
columnVV 
:VV 
$strVV "
)VV" #
;VV# $
migrationBuilderXX 
.XX 
CreateIndexXX (
(XX( )
nameYY 
:YY 
$strYY +
,YY+ ,
tableZZ 
:ZZ 
$strZZ !
,ZZ! "
column[[ 
:[[ 
$str[[ !
)[[! "
;[[" #
migrationBuilder]] 
.]] 
CreateIndex]] (
(]]( )
name^^ 
:^^ 
$str^^ )
,^^) *
table__ 
:__ 
$str__ 
,__ 
column`` 
:`` 
$str`` "
,``" #
uniqueaa 
:aa 
trueaa 
)aa 
;aa 
}bb 	
	protecteddd 
overridedd 
voiddd 
Downdd  $
(dd$ %
MigrationBuilderdd% 5
migrationBuilderdd6 F
)ddF G
{ee 	
migrationBuilderff 
.ff 
	DropTableff &
(ff& '
namegg 
:gg 
$strgg !
)gg! "
;gg" #
migrationBuilderii 
.ii 
	DropTableii &
(ii& '
namejj 
:jj 
$strjj  
)jj  !
;jj! "
migrationBuilderll 
.ll 
	DropTablell &
(ll& '
namemm 
:mm 
$strmm 
)mm 
;mm 
}nn 	
}oo 
}pp ≥ë
\/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Migrations/20210304101114_initial.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 

Migrations '
{ 
public 

partial 
class 
initial  
:! "
	Migration# ,
{ 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{		 	
migrationBuilder

 
.

 
AlterColumn

 (
<

( )
string

) /
>

/ 0
(

0 1
name 
: 
$str  
,  !
table 
: 
$str 
, 
type 
: 
$str 
, 
nullable 
: 
false 
,  

oldClrType 
: 
typeof "
(" #
string# )
)) *
,* +
oldType 
: 
$str 
)  
;  !
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str  
,  !
table 
: 
$str 
, 
type 
: 
$str 
, 
nullable 
: 
false 
,  

oldClrType 
: 
typeof "
(" #
string# )
)) *
,* +
oldType 
: 
$str 
)  
;  !
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str 
, 
table 
: 
$str 
, 
type 
: 
$str 
, 
nullable 
: 
false 
,  

oldClrType 
: 
typeof "
(" #
string# )
)) *
,* +
oldType   
:   
$str   
)    
;    !
migrationBuilder"" 
."" 
AlterColumn"" (
<""( )
int"") ,
>"", -
(""- .
name## 
:## 
$str## 
,## 
table$$ 
:$$ 
$str$$ 
,$$ 
type%% 
:%% 
$str%% 
,%%  
nullable&& 
:&& 
false&& 
,&&  

oldClrType'' 
:'' 
typeof'' "
(''" #
int''# &
)''& '
,''' (
oldType(( 
:(( 
$str(( "
)((" #
.)) 

Annotation)) 
()) 
$str)) <
,))< =)
NpgsqlValueGenerationStrategy))> [
.))[ \#
IdentityByDefaultColumn))\ s
)))s t
;))t u
migrationBuilder++ 
.++ 
AlterColumn++ (
<++( )
int++) ,
>++, -
(++- .
name,, 
:,, 
$str,, 
,,,  
table-- 
:-- 
$str-- !
,--! "
type.. 
:.. 
$str.. 
,..  
nullable// 
:// 
true// 
,// 

oldClrType00 
:00 
typeof00 "
(00" #
int00# &
)00& '
,00' (
oldType11 
:11 
$str11 "
,11" #
oldNullable22 
:22 
true22 !
)22! "
;22" #
migrationBuilder44 
.44 
AlterColumn44 (
<44( )
string44) /
>44/ 0
(440 1
name55 
:55 
$str55 
,55 
table66 
:66 
$str66 !
,66! "
type77 
:77 
$str77 
,77 
nullable88 
:88 
false88 
,88  

oldClrType99 
:99 
typeof99 "
(99" #
string99# )
)99) *
,99* +
oldType:: 
::: 
$str:: 
)::  
;::  !
migrationBuilder<< 
.<< 
AlterColumn<< (
<<<( )
int<<) ,
><<, -
(<<- .
name== 
:== 
$str== 
,==  
table>> 
:>> 
$str>> !
,>>! "
type?? 
:?? 
$str?? 
,??  
nullable@@ 
:@@ 
false@@ 
,@@  

oldClrTypeAA 
:AA 
typeofAA "
(AA" #
intAA# &
)AA& '
,AA' (
oldTypeBB 
:BB 
$strBB "
)BB" #
;BB# $
migrationBuilderDD 
.DD 
AlterColumnDD (
<DD( )
intDD) ,
>DD, -
(DD- .
nameEE 
:EE 
$strEE 
,EE  
tableFF 
:FF 
$strFF !
,FF! "
typeGG 
:GG 
$strGG 
,GG  
nullableHH 
:HH 
falseHH 
,HH  

oldClrTypeII 
:II 
typeofII "
(II" #
intII# &
)II& '
,II' (
oldTypeJJ 
:JJ 
$strJJ "
)JJ" #
;JJ# $
migrationBuilderLL 
.LL 
AlterColumnLL (
<LL( )
stringLL) /
>LL/ 0
(LL0 1
nameMM 
:MM 
$strMM &
,MM& '
tableNN 
:NN 
$strNN !
,NN! "
typeOO 
:OO 
$strOO 
,OO 
nullablePP 
:PP 
truePP 
,PP 

oldClrTypeQQ 
:QQ 
typeofQQ "
(QQ" #
stringQQ# )
)QQ) *
,QQ* +
oldTypeRR 
:RR 
$strRR 
,RR  
oldNullableSS 
:SS 
trueSS !
)SS! "
;SS" #
migrationBuilderUU 
.UU 
AlterColumnUU (
<UU( )
intUU) ,
>UU, -
(UU- .
nameVV 
:VV 
$strVV  
,VV  !
tableWW 
:WW 
$strWW !
,WW! "
typeXX 
:XX 
$strXX 
,XX  
nullableYY 
:YY 
falseYY 
,YY  

oldClrTypeZZ 
:ZZ 
typeofZZ "
(ZZ" #
intZZ# &
)ZZ& '
,ZZ' (
oldType[[ 
:[[ 
$str[[ "
)[[" #
;[[# $
migrationBuilder]] 
.]] 
AlterColumn]] (
<]]( )
int]]) ,
>]], -
(]]- .
name^^ 
:^^ 
$str^^ !
,^^! "
table__ 
:__ 
$str__ !
,__! "
type`` 
:`` 
$str`` 
,``  
nullableaa 
:aa 
falseaa 
,aa  

oldClrTypebb 
:bb 
typeofbb "
(bb" #
intbb# &
)bb& '
,bb' (
oldTypecc 
:cc 
$strcc "
)cc" #
.dd 

Annotationdd 
(dd 
$strdd <
,dd< =)
NpgsqlValueGenerationStrategydd> [
.dd[ \#
IdentityByDefaultColumndd\ s
)dds t
;ddt u
migrationBuilderff 
.ff 
AlterColumnff (
<ff( )
intff) ,
>ff, -
(ff- .
namegg 
:gg 
$strgg 
,gg 
tablehh 
:hh 
$strhh "
,hh" #
typeii 
:ii 
$strii 
,ii  
nullablejj 
:jj 
falsejj 
,jj  

oldClrTypekk 
:kk 
typeofkk "
(kk" #
intkk# &
)kk& '
,kk' (
oldTypell 
:ll 
$strll "
)ll" #
;ll# $
migrationBuildernn 
.nn 
AlterColumnnn (
<nn( )
intnn) ,
>nn, -
(nn- .
nameoo 
:oo 
$stroo 
,oo 
tablepp 
:pp 
$strpp "
,pp" #
typeqq 
:qq 
$strqq 
,qq  
nullablerr 
:rr 
falserr 
,rr  

oldClrTypess 
:ss 
typeofss "
(ss" #
intss# &
)ss& '
,ss' (
oldTypett 
:tt 
$strtt "
)tt" #
;tt# $
}uu 	
	protectedww 
overrideww 
voidww 
Downww  $
(ww$ %
MigrationBuilderww% 5
migrationBuilderww6 F
)wwF G
{xx 	
migrationBuilderyy 
.yy 
AlterColumnyy (
<yy( )
stringyy) /
>yy/ 0
(yy0 1
namezz 
:zz 
$strzz  
,zz  !
table{{ 
:{{ 
$str{{ 
,{{ 
type|| 
:|| 
$str|| 
,|| 
nullable}} 
:}} 
false}} 
,}}  

oldClrType~~ 
:~~ 
typeof~~ "
(~~" #
string~~# )
)~~) *
,~~* +
oldType 
: 
$str 
)  
;  !
migrationBuilder
ÅÅ 
.
ÅÅ 
AlterColumn
ÅÅ (
<
ÅÅ( )
string
ÅÅ) /
>
ÅÅ/ 0
(
ÅÅ0 1
name
ÇÇ 
:
ÇÇ 
$str
ÇÇ  
,
ÇÇ  !
table
ÉÉ 
:
ÉÉ 
$str
ÉÉ 
,
ÉÉ 
type
ÑÑ 
:
ÑÑ 
$str
ÑÑ 
,
ÑÑ 
nullable
ÖÖ 
:
ÖÖ 
false
ÖÖ 
,
ÖÖ  

oldClrType
ÜÜ 
:
ÜÜ 
typeof
ÜÜ "
(
ÜÜ" #
string
ÜÜ# )
)
ÜÜ) *
,
ÜÜ* +
oldType
áá 
:
áá 
$str
áá 
)
áá  
;
áá  !
migrationBuilder
ââ 
.
ââ 
AlterColumn
ââ (
<
ââ( )
string
ââ) /
>
ââ/ 0
(
ââ0 1
name
ää 
:
ää 
$str
ää 
,
ää 
table
ãã 
:
ãã 
$str
ãã 
,
ãã 
type
åå 
:
åå 
$str
åå 
,
åå 
nullable
çç 
:
çç 
false
çç 
,
çç  

oldClrType
éé 
:
éé 
typeof
éé "
(
éé" #
string
éé# )
)
éé) *
,
éé* +
oldType
èè 
:
èè 
$str
èè 
)
èè  
;
èè  !
migrationBuilder
ëë 
.
ëë 
AlterColumn
ëë (
<
ëë( )
int
ëë) ,
>
ëë, -
(
ëë- .
name
íí 
:
íí 
$str
íí 
,
íí 
table
ìì 
:
ìì 
$str
ìì 
,
ìì 
type
îî 
:
îî 
$str
îî 
,
îî  
nullable
ïï 
:
ïï 
false
ïï 
,
ïï  

oldClrType
ññ 
:
ññ 
typeof
ññ "
(
ññ" #
int
ññ# &
)
ññ& '
,
ññ' (
oldType
óó 
:
óó 
$str
óó "
)
óó" #
.
òò 
OldAnnotation
òò 
(
òò 
$str
òò ?
,
òò? @+
NpgsqlValueGenerationStrategy
òòA ^
.
òò^ _%
IdentityByDefaultColumn
òò_ v
)
òòv w
;
òòw x
migrationBuilder
öö 
.
öö 
AlterColumn
öö (
<
öö( )
int
öö) ,
>
öö, -
(
öö- .
name
õõ 
:
õõ 
$str
õõ 
,
õõ  
table
úú 
:
úú 
$str
úú !
,
úú! "
type
ùù 
:
ùù 
$str
ùù 
,
ùù  
nullable
ûû 
:
ûû 
true
ûû 
,
ûû 

oldClrType
üü 
:
üü 
typeof
üü "
(
üü" #
int
üü# &
)
üü& '
,
üü' (
oldType
†† 
:
†† 
$str
†† "
,
††" #
oldNullable
°° 
:
°° 
true
°° !
)
°°! "
;
°°" #
migrationBuilder
££ 
.
££ 
AlterColumn
££ (
<
££( )
string
££) /
>
££/ 0
(
££0 1
name
§§ 
:
§§ 
$str
§§ 
,
§§ 
table
•• 
:
•• 
$str
•• !
,
••! "
type
¶¶ 
:
¶¶ 
$str
¶¶ 
,
¶¶ 
nullable
ßß 
:
ßß 
false
ßß 
,
ßß  

oldClrType
®® 
:
®® 
typeof
®® "
(
®®" #
string
®®# )
)
®®) *
,
®®* +
oldType
©© 
:
©© 
$str
©© 
)
©©  
;
©©  !
migrationBuilder
´´ 
.
´´ 
AlterColumn
´´ (
<
´´( )
int
´´) ,
>
´´, -
(
´´- .
name
¨¨ 
:
¨¨ 
$str
¨¨ 
,
¨¨  
table
≠≠ 
:
≠≠ 
$str
≠≠ !
,
≠≠! "
type
ÆÆ 
:
ÆÆ 
$str
ÆÆ 
,
ÆÆ  
nullable
ØØ 
:
ØØ 
false
ØØ 
,
ØØ  

oldClrType
∞∞ 
:
∞∞ 
typeof
∞∞ "
(
∞∞" #
int
∞∞# &
)
∞∞& '
,
∞∞' (
oldType
±± 
:
±± 
$str
±± "
)
±±" #
;
±±# $
migrationBuilder
≥≥ 
.
≥≥ 
AlterColumn
≥≥ (
<
≥≥( )
int
≥≥) ,
>
≥≥, -
(
≥≥- .
name
¥¥ 
:
¥¥ 
$str
¥¥ 
,
¥¥  
table
µµ 
:
µµ 
$str
µµ !
,
µµ! "
type
∂∂ 
:
∂∂ 
$str
∂∂ 
,
∂∂  
nullable
∑∑ 
:
∑∑ 
false
∑∑ 
,
∑∑  

oldClrType
∏∏ 
:
∏∏ 
typeof
∏∏ "
(
∏∏" #
int
∏∏# &
)
∏∏& '
,
∏∏' (
oldType
ππ 
:
ππ 
$str
ππ "
)
ππ" #
;
ππ# $
migrationBuilder
ªª 
.
ªª 
AlterColumn
ªª (
<
ªª( )
string
ªª) /
>
ªª/ 0
(
ªª0 1
name
ºº 
:
ºº 
$str
ºº &
,
ºº& '
table
ΩΩ 
:
ΩΩ 
$str
ΩΩ !
,
ΩΩ! "
type
ææ 
:
ææ 
$str
ææ 
,
ææ 
nullable
øø 
:
øø 
true
øø 
,
øø 

oldClrType
¿¿ 
:
¿¿ 
typeof
¿¿ "
(
¿¿" #
string
¿¿# )
)
¿¿) *
,
¿¿* +
oldType
¡¡ 
:
¡¡ 
$str
¡¡ 
,
¡¡  
oldNullable
¬¬ 
:
¬¬ 
true
¬¬ !
)
¬¬! "
;
¬¬" #
migrationBuilder
ƒƒ 
.
ƒƒ 
AlterColumn
ƒƒ (
<
ƒƒ( )
int
ƒƒ) ,
>
ƒƒ, -
(
ƒƒ- .
name
≈≈ 
:
≈≈ 
$str
≈≈  
,
≈≈  !
table
∆∆ 
:
∆∆ 
$str
∆∆ !
,
∆∆! "
type
«« 
:
«« 
$str
«« 
,
««  
nullable
»» 
:
»» 
false
»» 
,
»»  

oldClrType
…… 
:
…… 
typeof
…… "
(
……" #
int
……# &
)
……& '
,
……' (
oldType
   
:
   
$str
   "
)
  " #
;
  # $
migrationBuilder
ÃÃ 
.
ÃÃ 
AlterColumn
ÃÃ (
<
ÃÃ( )
int
ÃÃ) ,
>
ÃÃ, -
(
ÃÃ- .
name
ÕÕ 
:
ÕÕ 
$str
ÕÕ !
,
ÕÕ! "
table
ŒŒ 
:
ŒŒ 
$str
ŒŒ !
,
ŒŒ! "
type
œœ 
:
œœ 
$str
œœ 
,
œœ  
nullable
–– 
:
–– 
false
–– 
,
––  

oldClrType
—— 
:
—— 
typeof
—— "
(
——" #
int
——# &
)
——& '
,
——' (
oldType
““ 
:
““ 
$str
““ "
)
““" #
.
”” 
OldAnnotation
”” 
(
”” 
$str
”” ?
,
””? @+
NpgsqlValueGenerationStrategy
””A ^
.
””^ _%
IdentityByDefaultColumn
””_ v
)
””v w
;
””w x
migrationBuilder
’’ 
.
’’ 
AlterColumn
’’ (
<
’’( )
int
’’) ,
>
’’, -
(
’’- .
name
÷÷ 
:
÷÷ 
$str
÷÷ 
,
÷÷ 
table
◊◊ 
:
◊◊ 
$str
◊◊ "
,
◊◊" #
type
ÿÿ 
:
ÿÿ 
$str
ÿÿ 
,
ÿÿ  
nullable
ŸŸ 
:
ŸŸ 
false
ŸŸ 
,
ŸŸ  

oldClrType
⁄⁄ 
:
⁄⁄ 
typeof
⁄⁄ "
(
⁄⁄" #
int
⁄⁄# &
)
⁄⁄& '
,
⁄⁄' (
oldType
€€ 
:
€€ 
$str
€€ "
)
€€" #
;
€€# $
migrationBuilder
›› 
.
›› 
AlterColumn
›› (
<
››( )
int
››) ,
>
››, -
(
››- .
name
ﬁﬁ 
:
ﬁﬁ 
$str
ﬁﬁ 
,
ﬁﬁ 
table
ﬂﬂ 
:
ﬂﬂ 
$str
ﬂﬂ "
,
ﬂﬂ" #
type
‡‡ 
:
‡‡ 
$str
‡‡ 
,
‡‡  
nullable
·· 
:
·· 
false
·· 
,
··  

oldClrType
‚‚ 
:
‚‚ 
typeof
‚‚ "
(
‚‚" #
int
‚‚# &
)
‚‚& '
,
‚‚' (
oldType
„„ 
:
„„ 
$str
„„ "
)
„„" #
;
„„# $
}
‰‰ 	
}
ÂÂ 
}ÊÊ ˙
M/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Pages/Error.cshtml.cs
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
Server

 
.

 
Pages

 "
{ 
[ 
ResponseCache 
( 
Duration 
= 
$num 
,  
Location! )
=* +!
ResponseCacheLocation, A
.A B
NoneB F
,F G
NoStoreH O
=P Q
trueR V
)V W
]W X
[ "
IgnoreAntiforgeryToken 
] 
public 

class 

ErrorModel 
: 
	PageModel '
{ 
public 
string 
	RequestId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
bool 
ShowRequestId !
=>" $
!% &
string& ,
., -
IsNullOrEmpty- :
(: ;
	RequestId; D
)D E
;E F
private 
readonly 
ILogger  
<  !

ErrorModel! +
>+ ,
_logger- 4
;4 5
public 

ErrorModel 
( 
ILogger !
<! "

ErrorModel" ,
>, -
logger. 4
)4 5
{ 	
_logger 
= 
logger 
; 
} 	
public 
void 
OnGet 
( 
) 
{ 	
	RequestId 
= 
Activity  
.  !
Current! (
?( )
.) *
Id* ,
??- /
HttpContext0 ;
.; <
TraceIdentifier< K
;K L
} 	
} 
}   ©
B/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Program.cs
	namespace 	
MiniTwitApi
 
. 
Server 
{ 
public 

class 
Program 
{ 
public 
static 
void 
Main 
(  
string  &
[& '
]' (
args) -
)- .
{ 	
CreateHostBuilder 
( 
args "
)" #
.# $
Build$ )
() *
)* +
.+ ,
Run, /
(/ 0
)0 1
;1 2
} 	
public 
static 
IHostBuilder "
CreateHostBuilder# 4
(4 5
string5 ;
[; <
]< =
args> B
)B C
=>D F
Host 
.  
CreateDefaultBuilder %
(% &
args& *
)* +
.   $
ConfigureWebHostDefaults   )
(  ) *

webBuilder  * 4
=>  5 7
{!! 

webBuilder"" 
."" 

UseStartup"" )
<"") *
Startup""* 1
>""1 2
(""2 3
)""3 4
.## 

UseKestrel## #
(### $
options##$ +
=>##, .
{$$ 
options%% #
.%%# $
Listen%%$ *
(%%* +
	IPAddress%%+ 4
.%%4 5
Any%%5 8
,%%8 9
$num%%: >
,%%> ?
listenOptions%%@ M
=>%%N P
{&& 
var''  #
serverCertificate''$ 5
=''6 7
LoadCertificate''8 G
(''G H
)''H I
;''I J
listenOptions((  -
.((- .
UseHttps((. 6
(((6 7
serverCertificate((7 H
)((H I
;((I J
})) 
))) 
;)) 
}** 
)** 
;** 
}++ 
)++ 
;++ 
private-- 
static-- 
X509Certificate2-- '
LoadCertificate--( 7
(--7 8
)--8 9
{.. 	
var// 
assembly// 
=// 
typeof// !
(//! "
Startup//" )
)//) *
.//* +
GetTypeInfo//+ 6
(//6 7
)//7 8
.//8 9
Assembly//9 A
;//A B
var00  
embeddedFileProvider00 $
=00% &
new00' * 
EmbeddedFileProvider00+ ?
(00? @
assembly00@ H
,00H I
$str00J ^
)00^ _
;00_ `
var11 
certificateFileInfo11 #
=11$ % 
embeddedFileProvider11& :
.11: ;
GetFileInfo11; F
(11F G
$str11G X
)11X Y
;11Y Z
using22 
(22 
var22 
certificateStream22 (
=22) *
certificateFileInfo22+ >
.22> ?
CreateReadStream22? O
(22O P
)22P Q
)22Q R
{33 
byte44 
[44 
]44 
certificatePayload44 )
;44) *
using55 
(55 
var55 
memoryStream55 '
=55( )
new55* -
MemoryStream55. :
(55: ;
)55; <
)55< =
{66 
certificateStream77 %
.77% &
CopyTo77& ,
(77, -
memoryStream77- 9
)779 :
;77: ;
certificatePayload88 &
=88' (
memoryStream88) 5
.885 6
ToArray886 =
(88= >
)88> ?
;88? @
}99 
return;; 
new;; 
X509Certificate2;; +
(;;+ ,
certificatePayload;;, >
,;;> ?
$str;;@ B
);;B C
;;;C D
}<< 
}== 	
}>> 
}?? ⁄	
d/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Repositories/Abstract/IFollowerRepository.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Repositories )
.) *
Abstract* 2
{ 
public 

	interface 
IFollowerRepository (
{		 
Task

 
<

 
ICollection

 
<

 
FollowerDTO

 $
>

$ %
>

% &
ReadAllAsync

' 3
(

3 4
string

4 :
username

; C
)

C D
;

D E
Task 
< 
int 
> 
DeleteAsync 
( 
FollowerDTO )
follower* 2
)2 3
;3 4
Task 
< 
int 
> 
CreateAsync 
( 
FollowerDTO )
follower* 2
)2 3
;3 4
Task 
< 
Follower 
> 
	ReadAsync  
(  !
string! '
whoUsername( 3
,3 4
string5 ;
whomUsername< H
)H I
;I J
} 
} Å	
c/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Repositories/Abstract/IMessageRepository.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Repositories )
.) *
Abstract* 2
{ 
public 

	interface 
IMessageRepository '
{		 
Task

 
CreateAsync

 
(

 

MessageDTO

 #
message

$ +
)

+ ,
;

, -
Task 
< 
ICollection 
< 

MessageDTO #
># $
>$ %
ReadAllAsync& 2
(2 3
int3 6
limit7 <
,< =
int> A
skipB F
)F G
;G H
Task 
< 
ICollection 
< 

MessageDTO #
># $
>$ %
ReadAllUserAsync& 6
(6 7
string7 =
username> F
,F G
intH K
limitL Q
,Q R
intS V
skipW [
)[ \
;\ ]
} 
} Œ	
`/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Repositories/Abstract/IUserRepository.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Repositories )
.) *
Abstract* 2
{ 
public 

	interface 
IUserRepository $
{		 
Task

 
<

 
bool

 
>

 
UserExistsAsync

 "
(

" #
string

# )
username

* 2
)

2 3
;

3 4
Task 
< 
bool 
> 
UserExistsAsync "
(" #
int# &
userid' -
)- .
;. /
Task 
CreateAsync 
( 
CreateUserDTO &
user' +
)+ ,
;, -
Task 
< 
UserDTO 
> 
	ReadAsync 
(  
int  #
userid$ *
)* +
;+ ,
Task 
< 
UserDTO 
> 
	ReadAsync 
(  
string  &
username' /
)/ 0
;0 1
} 
} ˝/
Z/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Repositories/FollowerRepository.cs
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
Server

 
.

 
Repositories

 )
{ 
public 

class 
FollowerRepository #
:$ %
IFollowerRepository& 9
{ 
private 
IContext 
_context !
{" #
get$ '
;' (
}) *
public 
FollowerRepository !
(! "
IContext" *
context+ 2
)2 3
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
Follower "
>" #
	ReadAsync$ -
(- .
string. 4
whoUsername5 @
,@ A
stringB H
whomUsernameI U
)U V
{ 	
var 
whoUser 
= 
await 
_context  (
.( )
Users) .
. 
Where 
( 
u 
=> 
u 
. 
Username &
.& '
Equals' -
(- .
whoUsername. 9
)9 :
): ;
. 
Select 
( 
u 
=> 
u 
) 
.  

FirstAsync  *
(* +
)+ ,
;, -
var 
whomUser 
= 
await  
_context! )
.) *
Users* /
. 
Where 
( 
u 
=> 
u 
. 
Username &
.& '
Equals' -
(- .
whomUsername. :
): ;
); <
. 
Select 
( 
u 
=> 
u 
) 
.  

FirstAsync  *
(* +
)+ ,
;, -
var 
relationship 
= 
await $
_context% -
.- .
	Followers. 7
.7 8
	FindAsync8 A
(A B
whoUserB I
.I J
UserIdJ P
,P Q
whomUserR Z
.Z [
UserId[ a
)a b
;b c
return   
relationship   
??    "
new  # &
Follower  ' /
(  / 0
)  0 1
{!! 
WhoId"" 
="" 
-"" 
$num"" 
,"" 
WhomId## 
=## 
-## 
$num## 
}$$ 
;$$ 
}%% 	
public'' 
async'' 
Task'' 
<'' 
ICollection'' %
<''% &
FollowerDTO''& 1
>''1 2
>''2 3
ReadAllAsync''4 @
(''@ A
string''A G
username''H P
)''P Q
{(( 	
return)) 
await)) 
()) 
from)) 
f))  
in))! #
_context))$ ,
.)), -
	Followers))- 6
where** 
f** 
.** 
Who** 
.** 
Username** $
.**$ %
Equals**% +
(**+ ,
username**, 4
)**4 5
select++ 
new++ 
FollowerDTO++ &
{,, 
WhoId-- 
=-- 
f-- 
.-- 
WhoId-- #
,--# $
WhomId.. 
=.. 
f.. 
... 
WhomId.. %
}// 
)// 
.// 
ToListAsync// 
(// 
)//  
;//  !
}00 	
public22 
async22 
Task22 
<22 
int22 
>22 
DeleteAsync22 *
(22* +
FollowerDTO22+ 6
follower227 ?
)22? @
{33 	
var44 
	_follower44 
=44 
await44 !
_context44" *
.44* +
	Followers44+ 4
.444 5
	FindAsync445 >
(44> ?
follower44? G
.44G H
WhoId44H M
,44M N
follower44O W
.44W X
WhomId44X ^
)44^ _
;44_ `
if66 
(66 
	_follower66 
is66 
null66  
)66  !
{77 
throw88 
new88 
ArgumentException88 +
(88+ ,
$"88, .A
5Could not remove follower, because it does not exist.88. c
"88c d
)88d e
;88e f
}99 
_context;; 
.;; 
	Followers;; 
.;; 
Remove;; %
(;;% &
	_follower;;& /
);;/ 0
;;;0 1
await== 
_context== 
.== 
SaveChangesAsync== +
(==+ ,
)==, -
;==- .
return>> 
follower>> 
.>> 
WhomId>> "
;>>" #
}?? 	
publicBB 
asyncBB 
TaskBB 
<BB 
intBB 
>BB 
CreateAsyncBB *
(BB* +
FollowerDTOBB+ 6
followerBB7 ?
)BB? @
{CC 	
varDD 
entityDD 
=DD 
newDD 
FollowerDD %
{EE 
WhoIdFF 
=FF 
followerFF  
.FF  !
WhoIdFF! &
,FF& '
WhomIdGG 
=GG 
followerGG !
.GG! "
WhomIdGG" (
}HH 
;HH 
awaitJJ 
_contextJJ 
.JJ 
	FollowersJJ $
.JJ$ %
AddAsyncJJ% -
(JJ- .
entityJJ. 4
)JJ4 5
;JJ5 6
awaitKK 
_contextKK 
.KK 
SaveChangesAsyncKK +
(KK+ ,
)KK, -
;KK- .
returnMM 
entityMM 
.MM 
WhoIdMM 
;MM  
}NN 	
}PP 
}QQ ß5
Y/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Repositories/MessageRepository.cs
	namespace 	
MiniTwitApi
 
. 
Server 
. 
Repositories )
{ 
public 

class 
MessageRepository "
:# $
IMessageRepository% 7
{ 
private 
IContext 
_context !
{" #
get$ '
;' (
}) *
public 
MessageRepository  
(  !
IContext! )
context* 1
)1 2
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
CreateAsync %
(% &

MessageDTO& 0
message1 8
)8 9
{ 	
var 
m 
= 
new 
Message 
(  
)  !
{ 
AuthorId 
= 
message "
." #
Author# )
,) *
AuthorUsername 
=  
message! (
.( )
AuthorUsername) 7
,7 8
Text 
= 
message 
. 
Text #
,# $
PubDate 
= 
message !
.! "
PublishDate" -
,- .
Flagged 
= 
message !
.! "
Flagged" )
} 
; 
var!! 
id!! 
=!! 
await!! 
_context!! #
.!!# $
Messages!!$ ,
.!!, -
AddAsync!!- 5
(!!5 6
m!!6 7
)!!7 8
;!!8 9
await"" 
_context"" 
."" 
SaveChangesAsync"" +
(""+ ,
)"", -
;""- .
}## 	
public%% 
async%% 
Task%% 
<%% 
ICollection%% %
<%%% &

MessageDTO%%& 0
>%%0 1
>%%1 2
ReadAllAsync%%3 ?
(%%? @
int%%@ C
limit%%D I
=%%J K
$num%%L N
,%%N O
int%%P S
skip%%T X
=%%Y Z
$num%%[ \
)%%\ ]
{&& 	
return'' 
await'' 
('' 
_context(( 
.(( 
Messages(( "
.)) 
OrderByDescending)) "
())" #
m))# $
=>))% '
m))( )
.))) *
PubDate))* 1
)))1 2
.** 
Skip** 
(** 
skip** 
)** 
.++ 
Take++ 
(++ 
limit++ 
)++ 
.,, 
Select,, 
(,, 
m,, 
=>,, 
new,,  

MessageDTO,,! +
(,,+ ,
),,, -
{-- 
Id.. 
=.. 
m.. 
... 
	MessageId.. $
,..$ %
Author// 
=// 
m// 
.// 
AuthorId// '
,//' (
AuthorUsername00 "
=00# $
m00% &
.00& '
AuthorUsername00' 5
,005 6
HashedAuthorEmail11 %
=11& '
UserDTO11( /
.11/ 0
MD5Hash110 7
(117 8
m118 9
.119 :
User11: >
.11> ?
Email11? D
)11D E
,11E F
Text22 
=22 
m22 
.22 
Text22 !
,22! "
PublishDate33 
=33  !
m33" #
.33# $
PubDate33$ +
,33+ ,
Flagged44 
=44 
m44 
.44  
Flagged44  '
}55 
)55 
)55 
.55 
ToListAsync55 
(55  
)55  !
;55! "
}66 	
public88 
async88 
Task88 
<88 
ICollection88 %
<88% &

MessageDTO88& 0
>880 1
>881 2
ReadAllUserAsync883 C
(88C D
string88D J
username88K S
,88S T
int88U X
limit88Y ^
=88_ `
$num88a c
,88c d
int88e h
skip88i m
=88n o
$num88p q
)88q r
{99 	
var:: 
userRepository:: 
=::  
new::! $
UserRepository::% 3
(::3 4
_context::4 <
)::< =
;::= >
var;; 
user;; 
=;; 
await;; 
userRepository;; +
.;;+ ,
	ReadAsync;;, 5
(;;5 6
username;;6 >
);;> ?
;;;? @
if<< 
(<< 
user<< 
is<< 
null<< 
)<< 
throw== 
new== 
ArgumentException== +
(==+ ,
$str==, N
)==N O
;==O P
return?? 
await?? 
(?? 
_context@@ 
.@@ 
Messages@@ !
.AA 
WhereAA 
(AA 
mAA 
=>AA 
mAA 
.AA 
AuthorUsernameAA ,
.AA, -
EqualsAA- 3
(AA3 4
usernameAA4 <
)AA< =
)AA= >
.BB 
OrderByDescendingBB "
(BB" #
mBB# $
=>BB% '
mBB( )
.BB) *
PubDateBB* 1
)BB1 2
.CC 
SkipCC 
(CC 
skipCC 
)CC 
.DD 
TakeDD 
(DD 
limitDD 
)DD 
.EE 
SelectEE 
(EE 
mEE 
=>EE 
newEE  

MessageDTOEE! +
(EE+ ,
)EE, -
{FF 
IdGG 
=GG 
mGG 
.GG 
	MessageIdGG $
,GG$ %
AuthorHH 
=HH 
mHH 
.HH 
AuthorIdHH '
,HH' (
AuthorUsernameII "
=II# $
mII% &
.II& '
AuthorUsernameII' 5
,II5 6
HashedAuthorEmailJJ %
=JJ& '
UserDTOJJ( /
.JJ/ 0
MD5HashJJ0 7
(JJ7 8
mJJ8 9
.JJ9 :
UserJJ: >
.JJ> ?
EmailJJ? D
)JJD E
,JJE F
TextKK 
=KK 
mKK 
.KK 
TextKK !
,KK! "
PublishDateLL 
=LL  !
mLL" #
.LL# $
PubDateLL$ +
,LL+ ,
FlaggedMM 
=MM 
mMM 
.MM  
FlaggedMM  '
}NN 
)NN 
)NN 
.NN 
ToListAsyncNN 
(NN  
)NN  !
;NN! "
}OO 	
}PP 
}QQ ¥+
V/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Repositories/UserRepository.cs
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
Server

 
.

 
Repositories

 )
{ 
public 

class 
UserRepository 
:  !
IUserRepository" 1
{ 
private 
IContext 
_context !
{" #
get$ '
;' (
}) *
public 
UserRepository 
( 
IContext &
context' .
). /
{ 	
_context 
= 
context 
; 
} 	
public 
async 
Task 
< 
bool 
> 
UserExistsAsync  /
(/ 0
string0 6
username7 ?
)? @
{ 	
return 
await 
	ReadAsync "
(" #
username# +
)+ ,
is- /
not0 3
null4 8
;8 9
} 	
public 
async 
Task 
< 
bool 
> 
UserExistsAsync  /
(/ 0
int0 3
userid4 :
): ;
{ 	
return 
await 
	ReadAsync "
(" #
userid# )
)) *
is+ -
not. 1
null2 6
;6 7
} 	
public 
async 
Task 
CreateAsync %
(% &
CreateUserDTO& 3
user4 8
)8 9
{   	
var!! 
id!! 
=!! 
await!! 
_context!! #
.!!# $
Users!!$ )
.!!) *
AddAsync!!* 2
(!!2 3
new"" 
User"" 
("" 
)"" 
{## 
Username$$ 
=$$ 
user$$ #
.$$# $
Username$$$ ,
,$$, -
Email%% 
=%% 
user%%  
.%%  !
Email%%! &
,%%& '
Password&& 
=&& 
user&& #
.&&# $
Password&&$ ,
,&&, -
	Followers'' 
='' 
new''  #
List''$ (
<''( )
Follower'') 1
>''1 2
(''2 3
)''3 4
,''4 5
Messages(( 
=(( 
new(( "
List((# '
<((' (
Message((( /
>((/ 0
(((0 1
)((1 2
,((2 3
})) 
))) 
;)) 
await** 
_context** 
.** 
SaveChangesAsync** +
(**+ ,
)**, -
;**- .
}++ 	
public-- 
async-- 
Task-- 
<-- 
UserDTO-- !
>--! "
	ReadAsync--# ,
(--, -
int--- 0
userid--1 7
)--7 8
{.. 	
var// 
user// 
=// 
await// 
_context// %
.//% &
Users//& +
.//+ ,
	FindAsync//, 5
(//5 6
userid//6 <
)//< =
;//= >
if11 
(11 
user11 
is11 
null11 
)11 
return22 
null22 
;22 
return44 
new44 
UserDTO44 
(44 
)44  
{55 
Id66 
=66 
user66 
.66 
UserId66  
,66  !
Email77 
=77 
user77 
.77 
Email77 "
,77" #
Password88 
=88 
user88 
.88  
Password88  (
,88( )
Username99 
=99 
user99 
.99  
Username99  (
}:: 
;:: 
};; 	
public== 
async== 
Task== 
<== 
UserDTO== !
>==! "
	ReadAsync==# ,
(==, -
string==- 3
username==4 <
)==< =
{>> 	
var?? 
user?? 
=?? 
from?? 
u?? 
in?? !
_context??" *
.??* +
Users??+ 0
where@@ 
u@@ 
.@@ 
Username@@  
.@@  !
Equals@@! '
(@@' (
username@@( 0
)@@0 1
selectAA 
newAA 
UserDTOAA "
(AA" #
)AA# $
{BB 
IdCC 
=CC 
uCC 
.CC 
UserIdCC !
,CC! "
EmailDD 
=DD 
uDD 
.DD 
EmailDD #
,DD# $
PasswordEE 
=EE 
uEE  
.EE  !
PasswordEE! )
,EE) *
UsernameFF 
=FF 
uFF  
.FF  !
UsernameFF! )
}GG 
;GG 
returnHH 
userHH 
.HH 
CountHH 
(HH 
)HH 
switchHH  &
{II 
$numJJ 
=>JJ 
nullJJ 
,JJ 
$numKK 
=>KK 
awaitKK 
userKK 
.KK  

FirstAsyncKK  *
(KK* +
)KK+ ,
,KK, -
_LL 
=>LL 
throwLL 
newLL 
	ExceptionLL (
(LL( )
$strLL) o
)LLo p
}MM 
;MM 
}NN 	
}QQ 
}RR ó+
B/home/joachim/git/PythonKindergarten/MiniTwitApi/Server/Startup.cs
	namespace 	
MiniTwitApi
 
. 
Server 
{ 
public 

class 
Startup 
{ 
public 
Startup 
( 
IConfiguration %
configuration& 3
)3 4
{ 	
Configuration 
= 
configuration )
;) *
} 	
public 
IConfiguration 
Configuration +
{, -
get. 1
;1 2
}3 4
public!! 
void!! 
ConfigureServices!! %
(!!% &
IServiceCollection!!& 8
services!!9 A
)!!A B
{"" 	
services## 
.## 
AddSystemMetrics## %
(##% &
)##& '
;##' (
services$$ 
.$$ #
AddControllersWithViews$$ ,
($$, -
)$$- .
;$$. /
services%% 
.%% 
AddRazorPages%% "
(%%" #
)%%# $
;%%$ %
services&& 
.&& 
	AddScoped&& 
<&& 
IContext&& '
,&&' (
Context&&) 0
>&&0 1
(&&1 2
)&&2 3
;&&3 4
services'' 
.'' 
	AddScoped'' 
<'' 
IUserRepository'' .
,''. /
UserRepository''0 >
>''> ?
(''? @
)''@ A
;''A B
services(( 
.(( 
	AddScoped(( 
<(( 
IFollowerRepository(( 2
,((2 3
FollowerRepository((4 F
>((F G
(((G H
)((H I
;((I J
services)) 
.)) 
	AddScoped)) 
<)) 
IMessageRepository)) 1
,))1 2
MessageRepository))3 D
>))D E
())E F
)))F G
;))G H
services** 
.** 
AddSwaggerGen** "
(**" #
)**# $
;**$ %
services-- 
.-- 
AddSingleton-- !
<--! ""
IActionContextAccessor--" 8
,--8 9!
ActionContextAccessor--: O
>--O P
(--P Q
)--Q R
;--R S
services00 
.00 
AddAuthentication00 &
(00& '(
CookieAuthenticationDefaults00' C
.00C D 
AuthenticationScheme00D X
)00X Y
.00Y Z
	AddCookie00Z c
(00c d
)00d e
;00e f
services11 
.11  
AddAuthorizationCore11 )
(11) *
)11* +
;11+ ,
}22 	
public55 
void55 
	Configure55 
(55 
IApplicationBuilder55 1
app552 5
,555 6
IWebHostEnvironment557 J
env55K N
)55N O
{66 	
if77 
(77 
env77 
.77 
IsDevelopment77 !
(77! "
)77" #
)77# $
{88 
app99 
.99 %
UseDeveloperExceptionPage99 -
(99- .
)99. /
;99/ 0
app:: 
.:: #
UseWebAssemblyDebugging:: +
(::+ ,
)::, -
;::- .
};; 
else<< 
{== 
app>> 
.>> 
UseExceptionHandler>> '
(>>' (
$str>>( 0
)>>0 1
;>>1 2
app@@ 
.@@ 
UseHsts@@ 
(@@ 
)@@ 
;@@ 
}AA 
appCC 
.CC 
UseHttpMetricsCC 
(CC 
)CC  
;CC  !
appDD 
.DD 
UseMetricServerDD 
(DD  
)DD  !
;DD! "
appEE 
.EE 
UseHttpsRedirectionEE #
(EE# $
)EE$ %
;EE% &
appFF 
.FF #
UseBlazorFrameworkFilesFF '
(FF' (
)FF( )
;FF) *
appGG 
.GG 
UseStaticFilesGG 
(GG 
)GG  
;GG  !
appJJ 
.JJ 

UseSwaggerJJ 
(JJ 
)JJ 
;JJ 
appNN 
.NN 
UseSwaggerUINN 
(NN 
cNN 
=>NN !
{OO 
cPP 
.PP 
SwaggerEndpointPP !
(PP! "
$strPP" <
,PP< =
$strPP> I
)PPI J
;PPJ K
}QQ 
)QQ 
;QQ 
appSS 
.SS 

UseRoutingSS 
(SS 
)SS 
;SS 
appUU 
.UU 
UseAuthenticationUU !
(UU! "
)UU" #
;UU# $
appVV 
.VV 
UseAuthorizationVV  
(VV  !
)VV! "
;VV" #
appXX 
.XX 
UseEndpointsXX 
(XX 
	endpointsXX &
=>XX' )
{YY 
	endpointsZZ 
.ZZ 
MapRazorPagesZZ '
(ZZ' (
)ZZ( )
;ZZ) *
	endpoints[[ 
.[[ 
MapControllers[[ (
([[( )
)[[) *
;[[* +
	endpoints\\ 
.\\ 
MapFallbackToFile\\ +
(\\+ ,
$str\\, 8
)\\8 9
;\\9 :
	endpoints]] 
.]] 

MapMetrics]] $
(]]$ %
)]]% &
;]]& '
}^^ 
)^^ 
;^^ 
}__ 	
}`` 
}aa 