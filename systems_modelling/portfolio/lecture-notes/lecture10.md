# Lecture 10 Exercises

### Event Storming Discussion
Pros:      
- As complexity grows, statecharts scale quite well
- Great for embedded systems requirements verification
- Great for modelling event driven objects that reacts to external events
- Apparently a state chart solves the state explosion problem (not sure - I think I saw this whilst reading up on state explosion)

Cons:      
- When there are a lot of different ideas, then it can be hard to manage.
- Not having the right stakeholders with knowledge of the domain will lead to a lot of irrelevant work

[Breakout Discussion](https://docs.google.com/document/d/17htmQzwsqXOdbt2k-ZyZolo0bzY8r9SaOhBzVpk4vNc/edit?usp=sharing)      
&nbsp;

### Project Leader Tasks
- Product owner is responsible for defining and refining requirements, also for interfacing with the client
- Architecture owner defines technical foundation of project according to requiremnts.
- Scum master is responsible for managing the tasks being done by development team      
&nbsp;

### Design Patterns
**Which patterns are presented?**      
- singleton
- facade
- adapter
- strategy
- observer

**Two favorites**   
- Facade pattern: It gives a simplistic interface for the developers to work with whilst masking underlying details. It also promotes loose coupling becuase there is low degree of interdependence between the classes.
- Observer pattern: The observer pattern is very widely used. It's quite similar to how web-sockets works on the frontend.   

**Two cons**   
- Facade pattern: it leaks details that it is supposed to abstract away. 
- Observer pattern: it can be difficult to debug as it is hard to keep track.  
