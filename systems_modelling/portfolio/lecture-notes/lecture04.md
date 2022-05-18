# Lecture 04 Exercises

## Exercise 1 - Class Diagram Example
**What do you like?**
- The class diagram is quite intuitive and easy to understand
- Well documented cardinalities

**What is curious?**
- The Transaction class shows that a transaction can have a type, however, it shows the Withdrawl class (which I assume might be a type) inheriting from the Transaction class. It is not clear from the diagram why that must exist
- Curious as to why the Money class is standalone. Would like to know why it is depicted that way.

**From Lecturer**: Cardinalities not so clear after all - Be careful about 1...1 relationships as it can mean that classes are the same thing.      
&nbsp;

## Exercise 2 - Class Diagram vs Domain Model
I had a hard time doing this exercise in class actually. I ususally need time to read and understand topics to really judge what's good and bad about them. I was still trying to read and understand, and could not do the discussion part with the team. 

**Advantages of Domain Model**
- Very useful in translating and interpreting requirements about a particular domain.
- Although domain models have OOP roots, they can be used with functional paradigms. I watched a talk on Domain driven design and F#.
- With types, in functional programming, we can have our design right in our code base which is great!!!
- Very useful in defining conceptual layer for the domain

**Confusing or Bad Design**:
- If the Domain diagram is a mix of class and object diagrams, I can see how too many details might be represented which can get quite confusing.      
&nbsp;

## Exercise 3 - Usecase/Persona Modelling Discussion
Link to breakout document [here](https://docs.google.com/document/d/1qgGjlMClRU1q5O7WqBxVs28q4FTj0QSGMVyXWI-tCSQ/edit)

**Pros of usecase diagrams**
- Great for describing functional requirements of the system
- Usecase diagrams are very easy to understand and as such are great for communicating with non-technical stakeholders

**Pros of personas**
- Great for reference purposes for new feature ideas
- Requirements engineers can use personas to come up with requirements for the system based on what their needs and motivations are

**Cons of usecase diagrams**
- Cannot be used to describe certain types of systems that do not require significant user input
- The lack of formality can be an issue as there is room for miscommunication and misunderstanding

**Cons of personas**
- Might be difficult to capture appropriate personas without considerable research
- There is the chance that personas might provide unreal and unreliable data      
&nbsp;

## Exercise 4 - Code Generation
**What was the most unexpected feature?** 
There was no unexpected feature. We have already done manual code generation in the past, so it is quite easy to follow.

**What was the hardest part to follow and why?** 
The hardest part to follow was the Bidirectional association between courses and location. I think the definition for this was not straightforward. 



