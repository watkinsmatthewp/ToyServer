# ToyServer

ToyServer is a small cross-platform .NET Core application that allows you to spin up a tiny server from the command line and log incoming HTTP requests.

## Building the app

1. Make sure you have .NET Core installed
1. Clone this repo
1. Open a command window in the repo directory
1. Run `dotnet build` in the directory

## Running the app

Just navigate to the repo and type `dotnet run`. You should see the server start up in your terminal:

```
C:\your-path\ToyServer>dotnet run
Hosting environment: Production
Content root path: C:\your-path\ToyServer
Now listening on: http://localhost:5000
Application started. Press Ctrl+C to shut down.
```

You can now start issuing HTTP requests to the localhost path and port specified. Raw HTTP request traffic will be written to the console. Also, each request will be written to a file in the ToyServer directory in your temp path. The HTTP response contains the path of the capture for that request.

## Example

### Example request

http://localhost:5000/path/to/page?id=4

### Example HTTP Request Catpure

```
GET http://localhost:5000/path/to/page?id=4 HTTP/1.1
Connection: keep-alive
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.8
Host: localhost:5000
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36
Upgrade-Insecure-Requests: 1
DNT: 1
```

### Example HTTP Response 

`Request logged to: C:\Users\your-user\AppData\Local\Temp\ToyServerRequests\2017-08-11-17-40-46.636380700463164071.http`
