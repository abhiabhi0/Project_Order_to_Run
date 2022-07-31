# Project_Order_to_Run

Consider a build system that is to build a set of projects. Projects may be dependent on each other, so they need to be built in order. The inputs will be:

-> List of projects, supplied as a list of strings: A1…An

-> List of dependencies, supplied as pairs: A1 depends on A2 and A3, etc.

Output: Any one valid build order of the projects.

### Examples

```
Input:
P1, P2, P3, P4
(P1, P2), (P1, P3), (P3, P4)

Output:
P2 P4 P3 P1
```

![ex1](https://github.com/programmercave0/Project_Order_to_Run/blob/main/Images/ex1.PNG)
