# How to: Modify packets

## Replace a value

To replace a value in a packet, call `Replace` and pass in the new value. A value of the specified
type will be replaced from the current position in the packet, and the packet's position will be
advanced after the new value.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=replace-single-value)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=replace-single-value)]

---

## Replace multiple values

You can replace multiple values by passing multiple arguments to `Replace`, which can be of
different types.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=replace-multiple-values)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=replace-multiple-values)]

---

## Modify a value

To modify a value in place, pass a function to `Modify`. A value of the specified type will be read
from the current position in the packet and supplied to your function, where you can perform any
transformations and return the modified value. The value in the packet will be replaced with the
modified value that you return. The packet's position will then be advanced after the newly replaced
value.

# [Minimal](#tab/minimal)

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=modify-single-value)]

# [Derived](#tab/derived)

[!code-csharp[](~/src/examples/packets/derived/MyExtension.cs?name=modify-single-value)]

---

## Modify multiple values

You can modify multiple values of different types by passing in multiple functions:

[!code-csharp[](~/src/examples/packets/minimal/Program.cs?name=modify-multiple-values)]