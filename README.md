# TL-Pokedex
A "True" Pokedex using an API "Layer"

## Prerequisites

- [.net5](https://dotnet.microsoft.com/download/dotnet/5.0)

### Optional 
- [Docker desktop](https://www.docker.com/products/docker-desktop) 

### How to run

**Clone the repo** 

First of all, let's clone the repo. 
You can do it via [Git](https://git-scm.com/) or simply downloading the [source code directly from GitHub](https://github.com/victor-rocha/TL-Pokedex)

**A few ways of running this service**

1. Visual Studio / Rider 

- Just simply navigate to `TL-Pokedex` and open `Pokedex.sln` file. 
- Hit 'Run' or 'Debug'
- Then navigate `https://localhost:5001/swagger/index.html` to use the API via [Swagger](https://swagger.io/)

2. Via `dotnet cli`

- Navigate to `TL-Pokedex/src/Pokedex.API` 
- Run `dotnet run --configuration Release --project Pokedex.API.csproj`
- Open your browser and navigate to `http://localhost:5000/swagger/index.html` to use the API via [Swagger](https://swagger.io/)

3. Via Docker

- Navigate to `TL-Pokedex`
- Run `docker build -t pokedex .`
- Then run `docker run -p 8080:80 pokedex`
- Last navigate to `http://localhost:8080/swagger/index.html` to use the API via [Swagger](https://swagger.io/)

## Development decisions
I usually like to try new things while doing home assignments like this. It allows me to try something new outside of my working hours and learning a bit more :)

This time around, I used [Hexagonal Architecture](https://blog.octo.com/en/hexagonal-architecture-three-principles-and-an-implementation-example/) as I was reading in some blogs. 
All the business logic are under `Pokedex.Domain` and the external queries (or outputs) are under `Pokedex.Queries`.

Moreover, another new stuff I've found was [Refit](https://github.com/reactiveui/refit) for creating the HttpClient for the external providers, and decided to use as well. 

## Things I'd do for a production API / Things to be improved

I tried to keep as simple as possible due to time constraints and, of course, this being a home assignment a not a fully production-ready API. 
I've listed a few things that I would implement if we would deploy to production:

- Implement a caching strategy such as MemCached or redis for instance. I've pointed out in the code. This also would include changes by adding a docker-compose for running all the required containers all together.  
- I'm filtering out English descriptions but I suppose we could make this more open. 
- Add some circuit-break/retry policy for the third party APIs. Something with [Polly](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly) would improve handling with errors, small timeouts, too many requests, etc. 
- Probably add an API versioning too depending on use-case. 
- Add some more logs throughout the application. Possibly implement a middleware that could help with that too.
- Add some tracing and observability (o11y) for incoming and outgoing requests and responses. 
- For dev/staging environment we could create a mocking service using [WireMock](http://wiremock.org/) so we don't rely on third parties for the external API responses. That would help development and testing across. 
- Improve and add more tests. Most of the unit tests cases were done but I would add some Integration Tests covering "real" API requests and responses.


