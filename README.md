# Air Quality Checker

A lightweight .NET web application which provides air quality data for locations obtained from the open-source [Open AQ API](https://docs.openaq.org/docs). 

## Prerequisites

The following tools are prerequisites to developing the website;

- Visual Studio 2022
- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Getting started

- Open Visual Studio
- Populate the appsettings.json file with
	- Redis cache connection string
	- Open AQ Api Key (optional, but increases request limit)
- Run the application

## Using the application

The home page displays a list of cities obtained from Open AQ.

Each city will have a number of locations which are home to an air-quality sensor.

You can view the locations in a city by either:
- Clicking the 'View locations' button next to a city
- Selecting a city from the dropdown and clicking 'Get air quality data for city'

You will then see a list of locations available for the city along with their coordinates.

To see the detailed air-quality readings for the location, click 'View data' next to the location.

Here you will see the latest measurement for each available air-quality parameter.

Once you've viewed a locations data, the page will be stored in the Recent Searches page.

## Technical details

When you first visit the application, a cookie will be generated for your "user id". This is used to store your search history in the 'Recent searches' page.

The app utilises caching of requests using Redis, so once you request the data for a city or location (which is requested from the Open AQ API) a cachekey is stored with the results of this request in Redis. 

When that same request is made again, the data is readily provided from the cache. 

The cached data is cleared after 24 hours (although this can be modified via the appsettings).




