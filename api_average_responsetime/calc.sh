#! /bin/bash

if [[ -z $1 ]]
then
    echo "host needs to be given eg. https://pythonkindergarten.tech"
    exit
fi


host=$1
output="results.txt"
average="average.sh"
max=$2

echo "AVERAGE RUNNING TIME FOR API ENDPOINTS ON MINITWIT APPLICATION" > $output
echo "####################################" > $output
echo "####################################" > $output
echo "####################################" > $output

#USER ENDPOINTS

bash $average "POST -d '{"username":"testingscript", "pwd":"testingscript"}' -H 'Content-Type: application/json' $host/login" $max >> $output #2.167s

bash $average "GET $host/user/1" $max >> $output

#bash $average "POST -d '{"username":"testingscript", "pwd":"testingscript", "email":"testingscript@test.dk"}' -H 'Content-Type: application/json' $host/register" $max >> $output


#LATEST ENDPOINT

bash $average "GET $host/latest" $max >> $output


#MESSAGE ENDPOINTS

bash $average "POST -d '{"content":"thisisatestingmessage"}' -H 'Content-Type: application/json' $host/msgs/testingscript" $max >> $output

bash $average "GET $host/msgs/testingscript?no=20" $max >> $output

bash $average "GET $host/msgs?no=100" $max >> $output

bash $average "GET $host/msgs?no=1000" $max >> $output

bash $average "GET $host/msgs?no=10000" $max >> $output

bash $average "GET $host/msgs?no=20" $max >> $output


#FOLLOW ENDPOINTS

bash $average "POST -d '{"follow":"jokk"}' -H 'Content-Type: application/json' $host/fllws/testingscript" $max >> $output

bash $average "GET $host/fllws/testingscript" $max >> $output

bash $average "POST -d '{"unfollow":"jokk"}' -H 'Content-Type: application/json' $host/fllws/testingscript" $max >> $output

bash $average "GET $host/fllws/testingscript?whoUserName=testingscript&whomUserName=jokk" $max >> $output