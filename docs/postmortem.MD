# Postmortem

Team B introduced a bug to our controller. After team B introduced this bug to controller, team A started their search for the bug. They used Kibana to search for the bug, where we have set up a dashboard catching different logging messages. After looking at the dashboard, team A were able to discover the bug: http://161.35.215.154:5601/app/kibana#/doc/2dcd3b70-8ee2-11eb-aafa-1d684dda3ce2/minitwitapi.server-2021-03/_doc?id=5gUzc3gBEkA_YTPKxeHE&_g=(refreshInterval:(pause:!t,value:0),time:(from:now-1d,to:now)) 

Reading of the message of the log, we could se that we failed to configure database context. They saw that there was an error in the MessageController, and the error message was “Failed to configure database context”. They looked up this error message in the code and found the placement of it. From there they fixed the error. 😊 
