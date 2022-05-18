# Lecture 03 Exercises

### Exercise 1 - Curiosities Contest
**Diagram 1**
- The object attributes don't show their type.
- The object names are not underlined.
- The relationship between objects isn't clear. It makes the diagram hard to understand.  
- For the deck object, we see that the property is an array. Whilst I don't think there's is anything bad with using an array as property, in this case, we see that separate card objects, with suit and value properties are defined. Curious to know why there is an array property. 
- The deck property name is "array", which is a value type.

**From Lecturer**: The arrows used in the diagram represents inheritance which is not used in object diagrams.     

**Diagram 2**
- The class and object names are switched.
- For the "Artjom" object, the name is capitalized.   
- The object attributes don't show their type.
- The object names are not underlined.

**Diagram 3**
- The object names are capitalized.
- The object attribute names are capitalized.
- The object names are not underlined. 
- The objects seem like classes e.g. The bank object with the name attribute seems like a class 
- It's a bit hard to understand what the diagram is trying to model, maybe with named associations?

**From Lecturer**: There should be consistency in the associations (No lonely arrows).       
**Thoughts**: What if having an arrow in the other relationships isn't an appropriate description of the relationship between the objects.

**Diagram 4**
- The object names are not capitalized.
- `pinCodeVerified`, a boolean value is represented as a string
- What's the difference between transaction objects and the withdrawal object?
- Also, if transaction1 and transaction2 are alike in terms of having a transaction type, why does only transaction1 have a relationship with a withdrawal object.
- The atrribute `name=value` map is inconsitent. It is `name=value` in some, and `name: value` in others.
- Does having transaction1 on a different line from the other transaction objects mean anything significant? (Lecturer mentioned this)
- The associations herw aren't intuitive enough.      
&nbsp;

### Exercise 2 - Class Diagram Discussion
The breakout session was a bit different. There was no moderator, but everyone conducted themselves in the best manner. We all gave very good points, and then used the public chat feature to outline the best props and cons. There were a lot more cons given than pros.       
This is a link to the document: [Breakout Document](https://docs.google.com/document/d/16EAnBcVnnrEuOjvpD7spuTkwRya6gWB7po8JWEFAlRM/edit)    

**Pro arguments**     
- Class diagrams are quite close to the implementation layer as it tells us what classes we need and the relationships between them. 

**Con arguments**     
- Getting class diagrams right can be challenging if you do not have enough experience. This is a problem as it means that we are miscommunicating. 
- Class diagrams model only the static structure of the system. 
&nbsp;

### Exercise 3 - Classes to Code the Manual Way
- **What was the most unexpected feature you saw me implementing and why?**     
    Everything was unexpected as I am quite new to OOP in any language. It seemed like there were "unspoken" rules to accessing variables in the classes.      
    
- **What was the hardest part to follow and why?**        
    The hardest part to follow was the Bi-directional associations. It was extremely hard to wrap my head around in the code-along. When I get around to doing it in the assignments, i'll get better at it. I think this was hard because I couldn't grasp the logic. I understood why it is happening, but I couldn't understand the associations in the conditions.
&nbsp;

