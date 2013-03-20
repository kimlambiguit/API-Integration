# Introduction
==============
The Tempurer API is developed using a RESTful Architecture. Representational State Transfer (or REST) is used in a client-server set up. The REST API allows the user to manipulate the state of the application by clicking on links. Each click results in a change in the state of the application.

The advantage of using a RESTful API is it easily integrates with the existing code. Very few changes need to be made to existing functions or code. All changes to the application are made by API calls.

However, before you can use the API in your applications, you will need to acquire an API Key.

# API-Integration
=================

## Getting a Tempurer API Key

1. Login and go to your Application List on the Tempurer site: <http://www.tempurer.com/api/applications>
2. Click the "Add Application" link near the top of the page.
3. Fill out the form to generate a **Consumer Key** and a **Consumer Secret**:
   - "Application Name" can be anything - preferably the name of the application being developed that will use the api data.
   - "Company URL" should be the url of the website displaying api data.
   - "oAuth Callback Domain" can be the same as Company URL (it isn't currently used but will be once 3 legged authentication is added in the future).

Get sample code
---------------

Get the PHP or C# sample code from https://github.com/tempurer/API-Integration

Enter your Consumer Key and Consumer Secret into the sample code in order to see an example of querying vacancies on the Tempurer platform

## API Calls
The following table lists and describes the API Calls in the Tempurer API:

<table>
<tr>
    <th>API Call</th>
    <th>Description</th>
</tr>
<tr>
    <td>List</td>
    <td>Returns a list of all Job Vacancies.</td>
</tr>
<tr>
    <td>Search</td>
    <td>Queries the list of Job Vacancies and returns the result of the query.</td>
</tr>
<tr>
    <td>Get</td>
    <td>Retrieves the details of a Job Vacancy.</td>
</tr>
<tr>
    <td>GetSummary</td>
    <td>Retrieves the summary of a Job Vacancy.</td>
</tr>
<tr>
    <td>listcountries</td>
    <td>Retrieves a list of all Country IDs.</td>
</tr>
<tr>
    <td>liststates</td>
    <td>Retrieves a list of all State IDs for a specific country.</td>
</tr>
<tr>
    <td>listsectors</td>
    <td>Retrieves a list of all the top level sectors.</td>
</tr>
<tr>
    <td>listsubsectors</td>
    <td>Retrieves a list of all the subsectors of a specific sector.</td>
</tr>
<tr>
    <td>localitybestmatch</td>
    <td>Tries to find a best match locality for the location radius search. It is loosely validated. (There is a lot of variation in the data. For example, some countries have postcodes, whereas some only have States. Thus, it is not possible to have strict validation.)<br/><br/>
localitybestmatch will return something even if you only pass in a countryid. However, it is recommended not to leave it at that. You will get better results if you also pass in a stateid, suburb, and postcode. The more information you pass in, the more accurate will be the results.</td>
</tr>
</table>

The following section describes each of the above API Calls in detail.

### List

Used to return a list job vacancies.

**URL:** `http://api.tempurer.com/services/vacancy.svc/list`

**Scopes:** `basic /services/vacancy.svc/list`

**Method:** `GET`

**Paramaters:**
<table>
<tr>
    <th>Paramater</th>
    <th>Required/<br/>Optional</th>
    <th>Is nullable?</th>
    <th>Description</th>
</tr>
<tr>
    <td>page</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Which page of the results to display. Defaults to page 1 (first page), if not specified.</td>
</tr>
<tr>
    <td>pagesize</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>The number of results to be displayed per page. Defaults to 10, if not specified.</td>
</tr>
</table>

**Output:**

TODO


### Search

Used to search for job vacancies.

**URL:** `http://api.tempurer.com/services/vacancy.svc/search`

**Scopes:** `basic /services/vacancy.svc/search`

**Method:** `GET`

**Paramaters:**
<table>
<tr>
    <th>Paramater</th>
    <th>Required/<br/>Optional</th>
    <th>Is nullable?</th>
    <th>Description</th>
</tr>
<tr>
    <td>query</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Matches on title. <font color="red">(What happens if no query is provided???; Also, What is meant by "matches on title"???)</font></td>
</tr>
<tr>
    <td>countryid</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>The Country ID as returned from listcountries. If provided, job vacancies of only the specified country will be retrieved. Otherwise, job vacancies of all countries will be retrieved.</td>
</tr>
<tr>
    <td>stateid</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>The State ID as returned from liststates method. If provided, job vacancies of only the specified State will be retrieved. Otherwise, job vacancies of all States will be retrieved.</td>
</tr>
<tr>
    <td>suburb</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Can be used to narrow down the search results. If provided, job vacancies of only the specified suburb will be retrieved. Otherwise, job vacancies of all suburbs will be retrieved.</td>
</tr>
<tr>
    <td>postcode</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Can be used to look for job vacancies in a particular area. If provided, job vacancies of only the specified postal code will be retrieved. Otherwise, job vacancies of all areas will be retrieved.</td>
</tr>
<tr>
    <td>localityid</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Can be used to look for job vacancies in a particular locality. If provided, job vacancies of only the specified locality will be retrieved. Otherwise, job vacancies of all localities will be retrieved.<br /><br />
You can either pass a localityid (in which case the country, state etc. will be ignored) or you can pass in the other fields which will attempt to resolve to a locality for the radius search. If no country id OR localityid is provided, the locality will be ignored and no attempt to resolve them will occur.</td>
</tr>
<tr>
    <td>radiuskms</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Can be used to look for job vacancies within a given distance from the user. If provided, job vacancies in localities that lie within the specified distance from the user will be retrieved. Otherwise, all job vacancies will be retrieved, irrespective of the distance from the user.</td>
</tr>
<tr>
    <td>sectorid</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Can be used to look for job vacancies in a particular sector (for example, 'Accountancy' or 'Software Development'). If provided, job vacancies of only the specified sector will be retrieved. Otherwise, job vacancies of all sectors will be retrieved.</td>
</tr>
<tr>
    <td>subsectorid</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Can be used to narrow down the list of job vacancies within a given sector (for example, to look for only 'Account Officer' or 'Java Programmers' vacancies. <b>A subsectorid can be used only if a sectorid is used.</b> If 'subsectorid' is provided, only job vacancies of the subsector will be retrieved. Otherwise, all job vacancies within the specified sector will be displayed.</td>
</tr>
<tr>
    <td>page</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Which page of the results to display. Defaults to page 1 (first page), if not specified.</td>
</tr>
</table>

**Output:**

TODO


### GetSummary

Used to return the summary of a job vacancy.

**URL:** `http://api.tempurer.com/services/vacancy.svc/getsummary`

**Scopes:** `basic /services/vacancy.svc/getsummary`

**Method:** ???

**Paramaters:**
<table>
<tr>
    <th>Paramater</th>
    <th>Required/<br/>Optional</th>
    <th>Is nullable?</th>
    <th>Description</th>
</tr>
<tr>
    <td>search</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Matches on title. <font color="red">(What is meant by "matches on title"???)</font> If 'search' is provided, it will return summaries of all entries that meet the search criteria. If 'search' is not provided, it will return summaries of <b>all</b> the job vacancies.</td>
</tr>
<tr>
    <td>page</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>Which page of the results to display. Defaults to page 1 (first page), if not specified.</td>
</tr>
<tr>
    <td>pagesize</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>The number of results to be displayed per page. Defaults to 10, if not specified.</td>
</tr>
</table>

**Output:**

TODO


### listsectors

Used to retrieve a list of all top level sectors.

**URL:** `http://api.tempurer.com/services/filters.svc/listsectors`

**Scopes:** `basic /services/filters.svc/listsectors`

**Method:** ???

**Paramaters:** None.

**Output:**
TODO


### listsubsectors

Used to retrieve a list of all the subsectors of a specific sector.

**URL:** `http://api.tempurer.com/services/filters.svc/listsubsectors/[sectorid]`

**Scopes:** `basic /services/filters.svc/listsubsectors`

**Method:** ???

**Paramaters:**
<table>
<tr>
    <th>Paramater</th>
    <th>Required/<br/>Optional</th>
    <th>Is nullable?</th>
    <th>Description</th>
</tr>
<tr>
    <td>sectorid (int)</td>
    <td><b>Required</b></td>
    <td>Yes</td>
    <td>The system will retrieve all the subsectors of the specified sector.</td>
</tr>
</table>

**Output:**

TODO


### listcountries

Used to retrieve a list of all countries.

**URL:** `http://api.tempurer.com/services/filters.svc/listcountries`

**Scopes:** `basic /services/filters.svc/listcountries`

**Method:** ???

**Paramaters:** None.

**Output:**

TODO


### liststates

Used to retrieve a list of all the States in a specific country.

**URL:** `http://api.tempurer.com/services/filters.svc/liststates/[countryid]`

**Scopes:** `basic /services/filters.svc/liststates`

**Method:** ???

**Paramaters:**
<table>
<tr>
    <th>Paramater</th>
    <th>Required/<br/>Optional</th>
    <th>Is nullable?</th>
    <th>Description</th>
</tr>
<tr>
    <td>countryid (int)</td>
    <td><b>Required</b></td>
    <td>Yes</td>
    <td>The system will retrieve all the States in the specified country.</td>
</tr>
</table>

**Output:**

TODO


### localitybestmatch

Tries to find a best match locality for the location radius search.

**URL:** `http://api.tempurer.com/services/filters.svc/localitybestmatch/[countryid]`

**Scopes:** `basic /services/filters.svc/localitybestmatch`

**Method:** ???

**Paramaters:**
<table>
<tr>
    <th>Paramater</th>
    <th>Required/<br/>Optional</th>
    <th>Is nullable?</th>
    <th>Description</th>
</tr>
<tr>
    <td>countryid (int)</td>
    <td><b>Required</b></td>
    <td>Yes</td>
    <td>The system will try to find a best-match locality for the specified country.</td>
</tr>
<tr>
    <td>stateid (int)</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>The system will try to find a best-match locality for the specified country, within the specified State.</td>
</tr>
<tr>
    <td>suburb (<font color="red">???</font>)</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>The system will try to find a best-match locality for the specified country, within the specified suburb.</td>
</tr>
<tr>
    <td>postcode (<font color="red">???</font>)</td>
    <td>Optional</td>
    <td>Yes</td>
    <td>The system will try to find a best-match locality for the specified country, within the specified postal code area.</td>
</tr>
</table>

Though only `countryid` is a required parameter, it is best to pass as many of the other parameters as possible to help the system get a match for the locality. The more the details provided, the more accurate will be the returned locality.

**Output:**

TODO