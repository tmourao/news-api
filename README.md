# hacker-news
This is a simple coding test API for consuming data from https://github.com/HackerNews/API


# run

- Open the solution in Visual Studio 2019
- Set News.API as default project
- Ctrl + F5 to run the project on IIS Express
- Test it with swagger

# changes

- Add exception handling + custom exception
- Add logging (built-in or Serilog)
- Add authentication using an API_KEY
- Add output cache mechanism if the data we are reading don't have real time changes
- Add a HttpClient helper to execute external APIs requests
- Have request URI details like API_Version in appsettings instead of hardcoded (e.g. /v0/)
- Create a develop branch
- Add unit testing project

Note: Depending on context or complexity some of the above points may not be needed


