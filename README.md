API-Integration
===============

Get a Tempurer API Key
----------------------

1. Login and go to your Application List on the Tempurer site: http://www.tempurer.com/api/applications
2. Click "Add Application"
3. Fill out the form to generate a Consumer Key and a Consumer Secret

Get sample code
---------------

Get the PHP or C# sample code from https://github.com/tempurer/API-Integration
Enter your Consumer Key and Consumer Secret into the sample code in order to query vacancies on the Tempurer platform


API Paramaters
--------------

api methods. page and pagesize are optional with pagesize defaulting to 10 if not specified.

list - page (optional), pagesize (optional)

search - query (required, matches on title), page (optional), pagesize (optional)

getsummary - search (optional, matches on title, returns all vacancies if empty), page (optional), pagesize (optional)

get/{guid} - no additional parameters
