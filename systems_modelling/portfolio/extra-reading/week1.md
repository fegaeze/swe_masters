# Object-Oriented Design (Week 1)

## Thoughts
- There is a need to understand when each UML diagram can be used. That is, the best diagram to communicate a specific issue.      
&nbsp;

## Object-Oriented Thinking
Object-oriented thinking is examining the problems and concepts at hand, breaking them down into components parts and thinking of them as objects. This basically means representing key concepts through objects. Each of these objects are self-aware, as they know specific details about themselves (properties). They also have specific responsibilities and behaviours that are relevant to the user of the object.    

In a software system, there can be three types of objects:      
**Entity Objects**: Correspond to some real-world entity in the problem space. These objects will know attributes about themselves. They will also be able to modify themselves, and have some rules for how to do so.      
**Boundary Objects**: Any object that deals with another system - a user, another software system, the Internet - can be considered a boundary object e.g User Interface elements      
**Control Objects**: Responsible for coordination. They control the activities of the other objects      

Note that your software will not solely consist of entity objects as there must also be objects for coordination and for interfacing with outside systems. Theses other objects are a little bit harder to see, but no less essential, especially as you move from small projects to more complex software.      
&nbsp;

**Why should we think in objects?**
- It keeps code organized by having related details and specific functions in distinct, easy to find places. 
- It creates flexibility because you can easily change details in a modular way without affecting the rest of the code. 
- We can also reuse code and keep our program simple.      
&nbsp;

### The role of design in the software process      
Problem Statement => **Elicit Requirements => Conceptual Model (Shared understanding) => Technical Design (Burrow into high level details)** => Implementation      
&nbsp;

**Elicit requirements**
- Prob clients to understand full scope of what is to be built. Sometimes, clients do not understand the big picture. By asking questions, we can get an accurate picture of what needs to be done.
- Establish potential tradeoffs with clients.
- User stories are one of many techniques that can be used to express requirements for a software system. They are very simple to use and can allow you to apply object-oriented thinking and discover objects and further requirements. Usually, the nouns in the stories correspond to objects in the software, and the verbs can help us identify the requirements that our objects might have.           
&nbsp;

**Conceptual Design**
- Created with initial requirements as basis
- Recognizes appropriate components, connections, and responsibilities of the software product. This is a high level outline, and to me, has a 1-to-1 mapping with the requirements from the user
- Useful for establishing a shared understanding of the system with the client, as well as figuring out loopholes in requuirement that need to be addressed
- Expressed or communicated through conceptual mock-ups (wireframes). These mock-ups do not outline technical details because that falls outside the scope of conceptual design      
&nbsp;

**Technical Design**
- Builds on conceptual designs and requirements to define the technical details of the solution
- It aims to describe how responsibilities of the components outlined in the conceptual design are met
- It is not finished until each components is split into smaller components that are specific enough to be designed in detail. By breaking down these components to the smallest form, we get down to a level where we can do a detailed design of a particular component. The final result is that each component will have their technical details specified.
- To communicate technical design, technical diagrams are used. Technical diagrams visualize how to address specific issues for each component. They become the basis for constructing the intended solution. Components at this stage may be refined enough to become collections of functions, classes, or other components      
&nbsp;

### Toy exercises   
**Exercise 1 - Point out objects in my study desk**
- Table
    - Behaviour: Holds all study items
    - Properties: color, width, height, material
- Desktop Monitor
    - Behaviour: Displays extra windows from laptop
    - Properties: resolution, color, manufacturer, 
- Laptop
    - Behaviour: Primary tool to do work tasks
    - Properties: color, manufacturer, OS
- Laptop Holder
    - Behaviour: Holds laptop
    - Properties: color, height, isAdjustible, canRotate
- Keyboard
    - Behaviour: Used to input text into the desktop and laptop
    - Properties: color, height, isAdjustible, canRotate, manufacturer, isOff, connectivity, batteryLevel
- Mouse 
    - Behaviour: Used to point and select items on desktop and laptop
    - Properties: color, manufacturer, isOff, connectivity, batteryLevel
- White Board
    - Behaviour: Used to write down during brainstorming sessions
    - Properties: material, isFilled, hasStickies      
&nbsp;

**Exercise 2 - Point out objects in laptop**
- Keyboard
- Touchbar
- Trackpad
- OS
- Screen
- Speaker
- Microphone
- USB Port
- Headphone jack
- Webcam      
&nbsp;