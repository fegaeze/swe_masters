# Lecture 08 Exercises

**What do we think about the size of a team for the final project?**      
Whilst I understand the need to move fast, and can see how having a large team can help with that problem. I think a smaller team would be better suited as the size of the project itself is not big enough to warrant the large team. It would also help foster communication and spur everyone into participation. It also would mean everyone would get their hands dirty in every aspect. It is also bothersome to follow up with lots of people.      
&nbsp;

**Are state charts better than plain (and documented) code?**      
Executable statecharts are actually quite useful for driving the actual run-time code, as well as generating a precise diagram. That being said, we get out of this plain code that is represented by a generated diagram. Note that, statecharts are not useful in every use case.       
&nbsp;


### Model Checking - [Reference](https://www.embedded.com/an-introduction-to-model-checking/)
- **Core concepts**   
The core concepts is that we usually have flawed design requirements which is peilous because it means that we introduce bugs into the system. Model checking involves using some kind of formal language to express and verfify the given requirements.             

- **How does this relate to state charts/state machines?**        
The formal language used to express the requirements are usually an extended form of finite state machines.         

- **What is really happening when checking a model?**       
According to the article in the heading, Using a model-checking tool, we accepts requirements (models) and properties (specifications) that is desired in the final system. If the model satisfies the specifications, then the tool outputs a success state. On the other hand, it generates a counterexample otherwise. By studying the counterexample, we can pinpoint the source of the error in the model, correct the model, and try again      

- **What is state explosion?**       
State machines often end up with a large number of states as more state variables are included for processing.      
&nbsp;

### State Charts
Pros:      
- As complexity grows, statecharts scale quite well
- Great for embedded systems requirements verification
- Great for modelling event driven objects that reacts to external events
- Apparently a state chart solves the state explosion problem (not sure - I think I saw this whilst reading up on state explosion)

Cons:      
- Complexity of the state diagram increases dramatically as the number of possible states increases
- It doesn't handle parallelism so well