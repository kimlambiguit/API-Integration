<?php 
/**
 *
 *	PHP API integration sample v1.0
 *
 *  The sample code below uses the Light PHP wrapper for the OAuth 2.0 library available from https://github.com/adoy/PHP-OAuth2
 *
 *	Steps:
 *  1) Get an api key and secret for your company by going to the application list at http://www.tempurer.com/API/Applications
 *	2) Set the  CLIENT_ID and CLIENT_SECRET values below
 *	
 *	Note that multiple scopes can be requested (separated by a space) in an Access Token request.  This allows the token to be re-used for different API calls.
 *
 */

	require('client.php');
	require('GrantType/IGrantType.php');
	require('GrantType/ClientCredentials.php');


	// Enter your API application credentials created at http://www.tempurer.com/API/Applications here:
	const CLIENT_ID     = 'TODO';
	const CLIENT_SECRET = 'TODO';
	

	// Constants to leave untouched
	const TOKEN_ENDPOINT       			= 'https://auth.tempurer.com/oauth2/token';
	const GRANT_TYPE 					= 'client_credentials';
	const AUTH_TYPE_AUTHORIZATION_BASIC = 1;
	const ACCESS_TOKEN_BEARER   		= 1;
	
	// Call api services
	ListVacancies();
	SearchVacancies();
	SummaryVacancies();
	GetVacancy();


	function ListVacancies()
	{
		$apiurl = 'http://api.tempurer.com/services/vacancy.svc/list';
		$scopes = 'basic /services/vacancy.svc/list';
		CallApiServiceUsingHttp($scopes, $apiurl);
	}


	function SearchVacancies()
	{
		$apiurl = 'http://api.tempurer.com/services/vacancy.svc/search?query=Test&page=1';
		$scopes = 'basic /services/vacancy.svc/search';
		CallApiServiceUsingHttp($scopes, $apiurl);
	}


	function SummaryVacancies()
	{
		$apiurl = 'http://api.tempurer.com/services/vacancy.svc/getsummary';
		$scopes = 'basic /services/vacancy.svc/getsummary';
		CallApiServiceUsingHttp($scopes, $apiurl);
	}


	function GetVacancy()
	{
		$apiurl = 'http://api.tempurer.com/services/vacancy.svc/get/faf03b36-b42e-e211-8a1b-842b2b6577a5';  // Set the guid of the vacancy you want to retrieve here.
		$scopes = 'basic /services/vacancy.svc/get';
		CallApiServiceUsingHttp($scopes, $apiurl);
	}


	// Generic function to call api after obtaining credentials from authorisation server.
	// Note that in a production system it would be more efficient to store the access token for reuse rather that obtaining it for every api call.
	function CallApiServiceUsingHttp($scopes, $apiurl)
	{
		// Create client object
		$client = new OAuth2\Client(CLIENT_ID, CLIENT_SECRET, AUTH_TYPE_AUTHORIZATION_BASIC);
		$client->setAccessTokenType(ACCESS_TOKEN_BEARER);

		// Get access token from authentication server
		$getAccessTokenParams = array('scope' => $scopes);
		$response = $client->getAccessToken(TOKEN_ENDPOINT, 'client_credentials', $getAccessTokenParams);
		$response_code = $response['code'];
		printf("%d", $response_code);
		
		printf("\n\nCALLING URL: %s WITH SCOPE: %s\n", $apiurl, $scopes);
		
		if ($response_code == 200) {
			// Success getting access token
			printf("SUCCESS GETTING TOKEN.\n");
			$access_token = $response['result']['access_token'];
			
			// Set access token on client object
			$client->setAccessToken($access_token);

			// Call api service
			$fetchParams = array('pagesize' => '1');
			$data = $client->fetch($apiurl, $fetchParams);
			var_dump($data);
		}
		else
		{
			// Error getting access token
			printf("ERROR OBTAINING TOKEN. CHECK CREDENTIALS, SCOPES AND TOKEN ENDPOINT.\n");
			//var_dump($response);
		}
	}

?> 