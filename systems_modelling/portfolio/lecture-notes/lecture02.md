# Lecture 02 Exercises

### Exercise 1 - Why do we need software architecture?
- Point of reference during development phase
- To better communicate to stakeholders system design
- To fish out and smoothen possible blockers in the modeling phase
- Better understanding of the system. This makes development phase easy.            
&nbsp;

### Exercise 2 - Find two pro arguments & two contra arguments for object diagrams      
Optional: relate to personal situations (could have helped or would have made things harder)     

**Pro arguments**     
- Great for modeling small parts of the system at a time. Maybe system functionality.
- Very easy to get started with, and easy for non-technical people to understand.
- Works closely with object-oriented concepts, so can be mapped easily to code.

**Con arguments**     
- Can be a nightmare if we try to create very large object diagrams. It becomes very messy and is no longer intuitive.
- Seems like it is only suited to Object-Oriennted Programming. How do we translate this to functional thinking?      
&nbsp;

### Exercise 3 - Discussion Warm-Up
I lost time drawing my diagram so I wasn't able to participate in the team discussion aspect. Ihar however finished on time, and shared his with the class.
<img src="../assets/lab02/enfa.png" alt="Enfa Object Diagram" width="500"/>          
&nbsp;

### Exercise 4 - Object Diagram Discussion
The breakout session was quite good. Everyone gave really good points. I volunteered as a note-taker for this session. This is a link to the document: [Breakout Document](https://docs.google.com/document/d/1BZ2dVX0412bKPMNV8SZ49XWBags3mcI_xxEpUXMAHMA/edit#heading=h.mfipt9rd7zi0 )       
&nbsp;

## Lecture Notes/Thoughts      

**Thoughts: Why do we need system models (even for TDD)?**      
When we watched the video, "Software Architecture vs. Code", and Simon Brown mentioned the point, "We do TDD!!!". I intially connected this point to mean that they don't do a big design of the software system up front because they are agile. Thinking about it again, I think that it can mean something else. I find that during TDD, developers need to have an expert understanding of the system and think critically about what they are trying to build. That being said, if we interprete software architecture as developers trying to design and understand the system, then we can conclude that they might see it as one or the other.

I don't think it's one or the other, I really think that they go well together. TDD is too focused on the code, and has nothing to do with communication.      
&nbsp;

**Thoughts: When is the software architecture good?**      
This question came up during the "class diagrams" labs. Are we making the right choices for the system? A couple of things from the Martin Fowler definition, and something Ihar said shapes a picture in my mind on this:
- "No two systems are the same". Each one has critical features that would influence design choices. Software architecture might be good if it considers these critical features and outline them
- Shared understanding. At the end of the day, software architecture is inherently a team effort. If everyone understands the system in a certain way and agrees on design choices, then we might be able to say that the software architecture is good for "our usecase". I think that's why there's a lot of back and forth in the labs when we discuss how best to model a scenario.         
&nbsp;

**Thoughts on object diagrams**      
- Working on the user stories have made me realize that object diagrams are like snapshots of a system functionality. It works great this way. Trying to use this to model the entire system will not give the desired results. I see them as a very useful guide for implementation as they usually model what the system should be if the functionality is to work. 
- The story-driven modeling book talks about an iterative approach to modeling. Can be Agile.
- I think system modeling in general encourges critical thinking. Very interesting :)      
- **Recommendation**: Use links instead of names whenever possible.  
&nbsp;

**Notes: Why we need software architecture?**
- For abstraction: To get an overview of parts of the system
- For better understanding (in design, deploy, and runtime phase)
- For testing – they can help defining and applying tests
- To facilitate communication between developers and all stakeholders
- To verify properties (fulfillment of requirements, correctness in regard of something)
- To structure: To make it reusable for other people e.g design patterns
- To make design practices repeatable (→ design patterns)
- To create, re-create code, understand code      
&nbsp;

