![](toy-server-header.jpg)

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
8/11/2017 18:10:04: Processing request from ::1
----------   BEGIN   ----------
GET http://localhost:5000/area/controller/action?id=5 HTTP/1.1
Cache-Control: max-age=0
Connection: keep-alive
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate, br
Accept-Language: en-US,en;q=0.8,es;q=0.6
Cookie: _ga=GA1.1.1680484914.1500667286
Host: localhost:5000
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36
Upgrade-Insecure-Requests: 1
DNT: 1


----------    END    ----------
```

### Example HTTP Response 

`Request logged to: C:\Users\your-user\AppData\Local\Temp\ToyServerRequests\2017-08-11-17-40-46.636380700463164071.http`

### Wait but why?

I had a case at work where one of our integration partners said we weren't including a certain header in our payloads when we hit their API. But while our our application traffic logging includes the "meat" of our HTTP traffic (URL, response codes, and payload), it doesn't log all the headers. And I wasn't about to go rewriting the shared logging component we use to include all that stuff and fill up our poor logging directory beyond the abuse we're already hitting it wtih when we only need it to answer this one partner one time. Also, I knew that when I told this partner we _were_ in fact sendin this header, I would need to include the raw HTTP byte for byte as proof.

First, I tried running the app locally and inspecting the traffic in Fiddler, but ran into a problem getting [Eric's instructions](https://www.telerik.com/blogs/capturing-traffic-from-.net-services-with-fiddler) to work on my box. So no dice there.

However, I _did_ have access to modify the endpoint URL the application uses. I could have created a new MVC website and gotten creative with that, but I was pressed for time and it seemed like a huge overhead to spin up a new web project and get it all configured to run locally just so I can inspect some headers. 

So I quickly wrote this stupid simple little console app. It has one job: log incoming HTTP requests byte for byte. Each request to one file. No overhead, no special processing-- that's it.
