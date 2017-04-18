# Rude.Net
Rude.Net is a C# implementation of the rule-based control-flow pattern [Rude](https://github.com/kaisersparpick/Rude).


## Usage

#### Creating an instance
```cs
var rude = new Rude();
```

#### Adding a rule

```cs
rude.AddRule(Condition, Yes, No);
```

`AddRule` accepts three arguments: the condition to check, the function to call when the result is true, and the function to call when it is false. Each argument is a `delegate` (a function pointer).

When a condition returns `null`, Rude exits the condition chain. These conditions are usually exit points.

Rules do not have to be added in linear order. The rules themselves determine the order the conditions are checked in. 
Rude automatically generates a key for each rule based on the condition function name -- therefore function names must be unique. 

#### Checking conditions

Checking conditions based on the applied rules is triggered by calling `CheckConditions()`.

```cs
rude.CheckConditions(MethodName);
```

This specifies the entry point in the condition chain and can be set to any valid rule name.

See a full application in the example folder.
