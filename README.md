# TL-Pokedex
A "True" Pokedex using an API "Layer"

## Prerequisites

- [.net5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Docker desktop](https://www.docker.com/products/docker-desktop)

### How to run

### Development decisions
I usually like to try new things while doing home assignments like this. It allows me to try something new outside of my working hours and learning a bit more :)

This time around, I used [Hexagonal Architecture](https://blog.octo.com/en/hexagonal-architecture-three-principles-and-an-implementation-example/) as I was reading in some blogs. While reading about it I also saw [Refit](https://github.com/reactiveui/refit) and decided to use as well. 


### Things I'd do for a production API

I tried to keep as simple as possible due to time constraints and, of course, this being a home assignment a not a fully production-ready API. 
I've listed a few things that I would implement if we would deploy to production:

- Implement a caching strategy such as MemCached or redis for instance. I've pointed out in the code. This also would include changes by adding a docker-compose. 
- I'm filtering out English descriptions but I suppose we could make this more open. 
- Add some circuit-break/retry policy for the third party APIs. Something with [Polly](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly) would improve handling with errors, small timeouts, too many requests, etc. 
- Probably add an API versioning too depending on use-case. 



