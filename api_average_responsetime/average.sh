#!/bin/bash

#Setting up averages for time calculations
realavg=0
useravg=0
sysavg=0

#Setup inputs
command=$1
max=$2
t=$( { time $(curl -X $command > /dev/null); } 2>&1 )
for ((i=0; i<$max;i++))
do
    #Calculate REAL time
    if [[ $t =~ real[[:space:]]*[[:digit:]]+m([[:digit:]]+[.][[:digit:]]+) ]];
    then
        realavg=$(echo "$realavg+${BASH_REMATCH[1]}" | bc)
    else
        echo "Error with Realtime"    
    fi

    #Calculate USER time
    if [[ $t =~ user[[:space:]]*[[:digit:]]+m([[:digit:]]+[.][[:digit:]]+) ]];
    then
        useravg=$(echo "$useravg+${BASH_REMATCH[1]}" | bc)
    else
        echo "Error with Usertime"    
    fi

    #Calculate SYS time
    if [[ $t =~ sys[[:space:]]*[[:digit:]]+m([[:digit:]]+[.][[:digit:]]+) ]];
    then
        sysavg=$(echo "$sysavg+${BASH_REMATCH[1]}" | bc)
    else
        echo "Error with SysTime"    
    fi
done

realavg=$(echo "scale=3;$realavg/$max" | bc)
useravg=$(echo "scale=3;$useravg/$max" | bc)
sysavg=$(echo "scale=3;$sysavg/$max" | bc)

echo "AVERAGE TIME RESULTS FOR $command"
echo "#################################"
echo "Real 0$realavg seconds"
echo "User 0$useravg seconds"
echo "Sys 0$sysavg seconds"
echo "#################################"
echo ""