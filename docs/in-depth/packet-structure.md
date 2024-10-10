# Structure of a packet

This article covers G-Earth's packet log, packet expression format and the different packet data
types.

## Reading packet logs

A packet has 3 parts - the length, header, and data. The length declares the number of bytes that
make up the header and data. The header value defines what type of message the packet represents,
for example, whether a user is talking, shouting, or whispering, whether a furni was placed, moved,
or removed, and so on. The data contains the information of the message, for example, the contents
of the chat message, what furni was placed and where, which furni was removed, etc.

Take a look at the following packet from G-Earth's packet logger.

```txt
[Chat]
Incoming[1064] -> [0][0][0]$[4]([0][0][0][0][0][12]Hello, world[0][0][0][0][0][0][0][0][0][0][0][0][0][0][0][0]
{in:Chat}{i:0}{s:"Hello, world"}{i:0}{i:0}{i:0}{i:0}
```

In the packet logger output, the first line has the message name, `Chat`.

The second line has the direction `Incoming` and header value `1064`, followed by a text
representation of the entire packet, including its length, header and data. This is known as the
[legacy packet format](#legacy-packet-format).

The third line represents the [packet as an expression](#packet-expression). G-Earth will attempt
to guess the structure of the packet for us and output a packet expression such as this one.

> [!NOTE]
> In xabbo, a @Xabbo.Messages.Header contains a @Xabbo.Messages.Header.Direction and
> @Xabbo.Messages.Header.Value. The length of a packet defines the length of the data only,
> excluding the header.
>
> On Shockwave, the length field is not present in the packet log. It starts with the 2-byte header
> (which itself is a [B64](#b64)), followed by the data.

> [!WARNING]
> The expression provided by G-Earth is not always 100% accurate as it is only a prediction of the
> packet's structure based on an algorithm. The expression may represent the same data, but not
> reflect the actual structure of the packet. For example, the integer `157801` could also be
> interpreted as the string `"hi"` - they both encode to the exact same bytes `00 02 68 69`
> (*on Flash*).

## Legacy packet format

From the example packet log above, the legacy packet format is as follows:

```txt
[0][0][0]$[4]([0][0][0][0][0][12]Hello, world[0][0][0][0][0][0][0][0][0][0][0][0][0][0][0][0]
```

Each individual character represents a single byte, with the exception of the numbers enclosed in
square brackets such as `[12]` which each represent the value of a single byte. These numbers are
used because a byte with that value does not represent a printable character. The square brackets
themselves must be encoded in the same way or it would be ambiguous as to whether it is a literal
square bracket or part of an encoded byte. Therefore, `[` becomes `[91]` and `]` becomes `[93]`.
Braces `{` `}` must also be encoded to `[123]` and `[125]` respectively as they are part of the
packet expression syntax.

The legacy format shows the entire packet, of which we can see the three parts:

- `[0][0][0]$` decodes to the integer `36`, representing the length of the header and data, and it
is always 4 bytes in length. This means that 36 more bytes follow. (36 is the ASCII value of the
`$` character)
- `[4](` is the header value, which decodes to `1064`. It is always 2 bytes in length.
- The rest of the text contains the packet data, in this case we have 34 bytes.

## Packet expression

Packet expressions provide an easier way to read the data in a packet. G-Earth will attempt to guess
the structure of the packet and output its expression. The first part `{in:Chat}` represents the
packet direction and message name. `{i:0}` represents an integer with the value `0`. Since an
integer is 4 bytes long, it is represented as `[0][0][0][0]` in the legacy packet format.
`{s:"Hello, world"}` represents the string of characters, `Hello, world`. Since strings are prefixed
by a [short](#short) to indicate their length, and `Hello, world` contains 12 characters, it is
represented as `[0][12]Hello, world` in the legacy packet format.

> [!NOTE]
> It is also possible to mix the legacy and packet expression formats. For example,
> `{i:1}{i:2}{i:3}` can be represented as `{i:1}[0][0][0][2]{i:3}`.

## Packet data types

### Boolean

A boolean takes up a single byte and can have the value $true$ or $false$. It is represented as
`{b:false}` or `{b:true}` in the packet expression format.

On Flash, a boolean is written as a [byte](#byte) with the value `0` or `1`.

On Shockwave, it is written as a [VL64](#vl64) with the value `0` or `1`.

#### Representations

# [Flash](#tab/flash)

+-------+--------+-------------+------+
| Value | Legacy | Expression  | Hex  |
+=======+========+=============+======+
| false | `[0]`  | `{b:false}` | `00` |
+-------+--------+-------------+------+
| true  | `[1]`  | `{b:true}`  | `01` |
+-------+--------+-------------+------+

# [Shockwave](#tab/shockwave)

+-------+--------+-------------+------+
| Value | Legacy | Expression  | Hex  |
+=======+========+=============+======+
| false | `H`    | `{b:false}` | `48` |
+-------+--------+-------------+------+
| true  | `I`    | `{b:true}`  | `49` |
+-------+--------+-------------+------+

---

### Byte

A byte value ranges from $0$ to $255$.
It is represented as a single character or a single number surrounded by square brackets in the
legacy packet format. For example, `[2]` would be the value `2`, and `A` would be the value `65`
(the ASCII value of the character `A`).

G-Earth uses the `ISO-8859-1` or `Latin1` character set to represent packet bytes in the legacy
packet format, so the value `255` would display as `ÿ`.

Bytes are rarely used in the Flash client, and not used in the Shockwave client.

#### Representations

+--------+---------+------------+------+
| Value  | Legacy  | Expression | Hex  |
+========+=========+============+======+
| `0`    | `[0]`   | `{b:0}`    | `00` |
+--------+---------+------------+------+
| `1`    | `[1]`   | `{b:1}`    | `01` |
+--------+---------+------------+------+
| `10`   | `[10]`  | `{b:10}`   | `0a` |
+--------+---------+------------+------+
| `100`  | `d`     | `{b:100}`  | `64` |
+--------+---------+------------+------+
| `144`  | `[144]` | `{b:144}`  | `90` |
+--------+---------+------------+------+
| `200`  | `È`     | `{b:200}`  | `c8` |
+--------+---------+------------+------+
| `255`  | `ÿ`     | `{b:255}`  | `ff` |
+--------+---------+------------+------+
| *Minimum value*                      |
+--------+---------+------------+------+
| `0`    | `[0]`   | `{b:0}`    | `00` |
+--------+---------+------------+------+
| *Maximum value*                      |
+--------+---------+------------+------+
| `255`  | `ÿ`     | `{b:255}`  | `ff` |
+--------+---------+------------+------+

### Short

On Flash, a short is 2 bytes long and ranges from $-32768$ to $32767$.

On Shockwave, shorts are encoded to [B64](#b64) which are also 2 bytes, however their encoding is
completely different.

#### Representations

# [Flash](#tab/short-flash)

+----------+-------------+-----------------+---------+
| Value    | Legacy      | Expression      | Hex     |
+==========+=============+=================+=========+
| `0`      | `[0][0]`    | `{u:0}`         | `00 00` |
+----------+-------------+-----------------+---------+
| `1`      | `[0][1]`    | `{u:1}`         | `00 01` |
+----------+-------------+-----------------+---------+
| `10`     | `[0][10]`   | `{u:10}`        | `00 0a` |
+----------+-------------+-----------------+---------+
| `100`    | `[0]d`      | `{u:100}`       | `00 64` |
+----------+-------------+-----------------+---------+
| `200`    | `[0]È`      | `{u:200}`       | `00 c8` |
+----------+-------------+-----------------+---------+
| `255`    | `[0]ÿ`      | `{u:255}`       | `00 ff` |
+----------+-------------+-----------------+---------+
| `256`    | `[1][0]`    | `{u:256}`       | `01 00` |
+----------+-------------+-----------------+---------+
| `4000`   | `[15] `[^1] | `{u:4000}`      | `0f a0` |
+----------+-------------+-----------------+---------+
| `4095`   | `[15]ÿ`     | `{u:4095}`      | `0f ff` |
+----------+-------------+-----------------+---------+
| `4096`   | `[16][0]`   | `{u:4096}`      | `10 00` |
+----------+-------------+-----------------+---------+
| `-1`     | `ÿÿ`        | `{u:65535}`[^2] | `ff ff` |
+----------+-------------+-----------------+---------+
| *Minimum value (signed)*                           |
+----------+-------------+-----------------+---------+
| `-32768` | `[128][0]`  | `{u:32768}`[^3] | `80 00` |
+----------+-------------+-----------------+---------+
| *Maximum value (signed)*                           |
+----------+-------------+-----------------+---------+
| `32767`  | `[127]ÿ`    | `{u:32767}`     | `7f ff` |
+----------+-------------+-----------------+---------+

# [Shockwave (B64)](#tab/short-shockwave)

+----------+--------------+-----------------+---------+
| Value    | Legacy       | Expression      | Hex     |
+==========+==============+=================+=========+
| `0`      | `@@`         | `{u:0}`         | `40 40` |
+----------+--------------+-----------------+---------+
| `1`      | `@A`         | `{u:1}`         | `40 41` |
+----------+--------------+-----------------+---------+
| `10`     | `@J`         | `{u:10}`        | `40 4a` |
+----------+--------------+-----------------+---------+
| `63`     | `@[127]`     | `{u:63}`        | `40 7f` |
+----------+--------------+-----------------+---------+
| `64`     | `A@`         | `{u:64}`        | `41 40` |
+----------+--------------+-----------------+---------+
| `128`    | `B@`         | `{u:128}`       | `42 40` |
+----------+--------------+-----------------+---------+
| `200`    | `CH`         | `{u:200}`       | `43 48` |
+----------+--------------+-----------------+---------+
| `255`    | `C[127]`     | `{u:255}`       | `43 7f` |
+----------+--------------+-----------------+---------+
| `256`    | `D@`         | `{u:256}`       | `44 40` |
+----------+--------------+-----------------+---------+
| `4000`   | `` ~` ``     | `{u:4000}`      | `7e 60` |
+----------+--------------+-----------------+---------+
| `4095`   | `[127][127]` | `{u:4095}`      | `7f 7f` |
+----------+--------------+-----------------+---------+
| `4096`   | *Cannot be represented*                  |
+----------+--------------+-----------------+---------+
| `-1`     | *Cannot be represented*                  |
+----------+--------------+-----------------+---------+
| *Minimum value*                                     |
+----------+--------------+-----------------+---------+
| `0`      | `@@`         | `{u:0}`         | `80 00` |
+----------+--------------+-----------------+---------+
| *Maximum value*                                     |
+----------+--------------+-----------------+---------+
| `4095`   | `[127][127]` | `{u:4095}`      | `0f ff` |
+----------+--------------+-----------------+---------+

---

### Int

On Flash, an int is 4 bytes long and ranges from $-2147483648$ to $2147483647$.

On Shockwave, integers are encoded to [VL64](#vl64) which have a variable length, and a slightly
different range.

#### Representations

# [Flash](#tab/int-flash)

+------------------+------------------+-------------------+---------------+
| Value            | Legacy           | Expression        | Hex           |
+==================+==================+===================+===============+
| `0`              | `[0][0][0][0]`   | `{i:0}`           | `00 00 00 00` |
+------------------+------------------+-------------------+---------------+
| `1`              | `[0][0][0][1]`   | `{i:1}`           | `00 00 00 01` |
+------------------+------------------+-------------------+---------------+
| `2`              | `[0][0][0][2]`   | `{i:2}`           | `00 00 00 02` |
+------------------+------------------+-------------------+---------------+
| `3`              | `[0][0][0][3]`   | `{i:3}`           | `00 00 00 03` |
+------------------+------------------+-------------------+---------------+
| `4`              | `[0][0][0][4]`   | `{i:4}`           | `00 00 00 04` |
+------------------+------------------+-------------------+---------------+
| `255`            | `[0][0][0]ÿ`     | `{i:255}`         | `00 00 00 ff` |
+------------------+------------------+-------------------+---------------+
| `256`            | `[0][0][1][0]`   | `{i:256}`         | `00 00 01 00` |
+------------------+------------------+-------------------+---------------+
| `16777215`       | `[0]ÿÿÿ`         | `{i:16777215}`    | `00 ff ff ff` |
+------------------+------------------+-------------------+---------------+
| `49848964`       | `[2]ø¢[132]`     | `{i:49848964}`    | `02 f8 a2 84` |
+------------------+------------------+-------------------+---------------+
| `2147418112`[^4] | `[127]ÿ[0][0]`   | `{i:2147418112}`  | `7f ff 00 00` |
+------------------+------------------+-------------------+---------------+
| `-1`             | `ÿÿÿÿ`           | `{i:-1}`          | `ff ff ff ff` |
+------------------+------------------+-------------------+---------------+
| `-2`             | `ÿÿÿþ`           | `{i:-2}`          | `ff ff ff fe` |
+------------------+------------------+-------------------+---------------+
| `-3`             | `ÿÿÿý`           | `{i:-3}`          | `ff ff ff fd` |
+------------------+------------------+-------------------+---------------+
| `-4`             | `ÿÿÿü`           | `{i:-4}`          | `ff ff ff fc` |
+------------------+------------------+-------------------+---------------+
| *Minimum value*                                                         |
+------------------+------------------+-------------------+---------------+
| `-2147483648`    | `[128][0][0][0]` | `{i:-2147483648}` | `80 00 00 00` |
+------------------+------------------+-------------------+---------------+
| *Maximum value*                                                         |
+------------------+------------------+-------------------+---------------+
| `2147483647`     | `[127]ÿÿÿ`       | `{i:2147483647}`  | `7f ff ff ff` |
+------------------+------------------+-------------------+---------------+

# [Shockwave (VL64)](#tab/int-shockwave)

+---------------+--------------------------+-------------------+---------------------+
| Value         | Legacy                   | Expression        | Hex                 |
+===============+==========================+===================+=====================+
| `0`           | `H`                      | `{i:0}`           | `48`                |
+---------------+--------------------------+-------------------+---------------------+
| `1`           | `I`                      | `{i:1}`           | `49`                |
+---------------+--------------------------+-------------------+---------------------+
| `2`           | `J`                      | `{i:2}`           | `4a`                |
+---------------+--------------------------+-------------------+---------------------+
| `3`           | `K`                      | `{i:3}`           | `4b`                |
+---------------+--------------------------+-------------------+---------------------+
| `4`           | `PA`                     | `{i:4}`           | `50 41`             |
+---------------+--------------------------+-------------------+---------------------+
| `255`         | `S[127]`                 | `{i:255}`         | `53 7f`             |
+---------------+--------------------------+-------------------+---------------------+
| `256`         | `X@A`                    | `{i:256}`         | `58 40 41`          |
+---------------+--------------------------+-------------------+---------------------+
| `16777215`    | `k[127][127][127]O`      | `{i:16777215}`    | `6b 7f 7f 7f 4f`    |
+---------------+--------------------------+-------------------+---------------------+
| `49848964`    | `habbo`                  | `{i:49848964}`    | `68 61 62 62 6f`    |
+---------------+--------------------------+-------------------+---------------------+
| `2147418112`  | `p@@|[127]_`             | `{i:2147418112}`  | `70 40 40 7c 7f 5f` |
+---------------+--------------------------+-------------------+---------------------+
| `-1`          | `M`                      | `{i:-1}`          | `4d`                |
+---------------+--------------------------+-------------------+---------------------+
| `-2`          | `N`                      | `{i:-2}`          | `4e`                |
+---------------+--------------------------+-------------------+---------------------+
| `-3`          | `O`                      | `{i:-3}`          | `4f`                |
+---------------+--------------------------+-------------------+---------------------+
| `-4`          | `TA`                     | `{i:-4}`          | `54 41`             |
+---------------+--------------------------+-------------------+---------------------+
| *Minimum value*                                                                    |
+---------------+--------------------------+-------------------+---------------------+
| `-2147483647` | `w[127][127][127][127]_` | `{i:-2147483647}` | `77 7f 7f 7f 7f 5f` |
+---------------+--------------------------+-------------------+---------------------+
| *Maximum value*                                                                    |
+---------------+--------------------------+-------------------+---------------------+
| `2147483647`  | `s[127][127][127][127]_` | `{i:2147483647}`  | `73 7f 7f 7f 7f 5f` |
+---------------+--------------------------+-------------------+---------------------+

---

### Float

On the Unity client, a single-precision floating point number is 4 bytes long. Floats can represent
real numbers such as `3.14` and are often used for the Z or height coordinate of avatars and furni
in a room.

On Flash and Shockwave, `Read<float>()` will read a string and parse it into a float, while
`Write(float)` will format the float to a string and write it to the packet.

### Long

A long is 8 bytes long and ranges from $-9223372036854775808$ to $9223372036854775807$.

This type is very rarely used in the Flash client. It was introduced in the Unity client which
changed the numeric IDs of users, groups, furni etc. to this type which can hold a much larger range
of values.

#### Representations

+------------------------+------------------------------+----------------------------+---------------------------+
| Value                  | Legacy                       | Expression                 | Hex                       |
+========================+==============================+============================+===========================+
| `0`                    | `[0][0][0][0][0][0][0][0]`   | `{i:0}`                    | `00 00 00 00 00 00 00 00` |
+------------------------+------------------------------+----------------------------+---------------------------+
| `1`                    | `[0][0][0][0][0][0][0][1]`   | `{i:1}`                    | `00 00 00 00 00 00 00 01` |
+------------------------+------------------------------+----------------------------+---------------------------+
| `255`                  | `[0][0][0][0][0][0][0]ÿ`     | `{i:10}`                   | `00 00 00 00 00 00 00 0a` |
+------------------------+------------------------------+----------------------------+---------------------------+
| `65535`                | `[0][0][0][0][0][0]ÿÿ`       | `{i:32767}`                | `00 00 00 00 00 00 7f ff` |
+------------------------+------------------------------+----------------------------+---------------------------+
| `4294967295`           | `[0][0][0][0]ÿÿÿÿ`           | `{i:2147418112}`           | `00 00 00 00 7f ff 00 00` |
+------------------------+------------------------------+----------------------------+---------------------------+
| `-1`                   | `ÿÿÿÿÿÿÿÿ`                   | `{i:-1}`                   | `ff ff ff ff ff ff ff ff` |
+------------------------+------------------------------+----------------------------+---------------------------+
| *Minimum value*                                                                                                |
+------------------------+------------------------------+----------------------------+---------------------------+
| `-9223372036854775808` | `[128][0][0][0][0][0][0][0]` | `{l:-9223372036854775808}` | `80 00 00 00 00 00 00 00` |
+------------------------+------------------------------+----------------------------+---------------------------+
| *Maximum value*                                                                                                |
+------------------------+------------------------------+----------------------------+---------------------------+
| `9223372036854775807`  | `[127]ÿÿÿÿÿÿÿ`               | `{l:9223372036854775807}`  | `7f ff ff ff ff ff ff ff` |
+------------------------+------------------------------+----------------------------+---------------------------+

### String

A string is a sequence of characters that make up text, such as `"Hello, world"`.

On Unity, Flash, and in *outgoing* Shockwave packets, strings are prefixed with a [short](#short)
that indicates the length of the string in bytes. On Shockwave, this will be written as a
[B64](#b64).

For example, the string `"hello"` is 5 bytes long, so a `short` with the value `5` will be written
before the characters of the string. In the legacy packet format, this will be represented as
`[0][5]hello` on modern clients, and `@Ehello` on Shockwave, where `@E` decodes to the value `5`.

Strings are handled differently in *incoming* Shockwave packets. They do not have a length prefix
and are terminated by the byte `0x02` or `[2]`. This means that to read a string from an incoming
Shockwave packet, you must read each byte until you reach a byte with the value `2`. For example,
`"hello"` will be represented as `hello[2]` in the legacy packet format.

Shockwave often splits strings by the tab character `\t` or `[9]`, and lines by the carriage-return
character `\r` or `[13]`.

#### Representations

# [Flash](#tab/string-flash)

+------------------+-------------------------+----------------------+
| Value            | Legacy                  | Expression           |
+==================+=========================+======================+
| `""`             | `[0][0]`                | `{s:""}`             |
+------------------+-------------------------+----------------------+
| `"hi"`           | `[0][2]hi`              | `{s:"hi"}`           |
+------------------+-------------------------+----------------------+
| `"hello"`        | `[0][5]hello`           | `{s:"hello"}`        |
+------------------+-------------------------+----------------------+
| `"hello world"`  | `[0][11]hello world`    | `{s:"hello world"}`  |
+------------------+-------------------------+----------------------+
| `"hello\tworld"` | `[0][11]hello[9]world`  | `{s:"hello\tworld"}` |
+------------------+-------------------------+----------------------+
| `"hello\rworld"` | `[0][11]hello[13]world` | `{s:"hello\rworld"}` |
+------------------+-------------------------+----------------------+

# [Shockwave (Outgoing)](#tab/string-shockwave-out)

+------------------+--------------------+----------------------+
| Value            | Legacy             | Expression           |
+==================+====================+======================+
| `""`             | `@@`               | `{s:""}`             |
+------------------+--------------------+----------------------+
| `"hi"`           | `@Bhi`             | `{s:"hi"}`           |
+------------------+--------------------+----------------------+
| `"hello"`        | `@Ehello`          | `{s:"hello"}`        |
+------------------+--------------------+----------------------+
| `"hello world"`  | `@Khello world`    | `{s:"hello world"}`  |
+------------------+--------------------+----------------------+
| `"hello\tworld"` | `@Khello[9]world`  | `{s:"hello\tworld"}` |
+------------------+--------------------+----------------------+
| `"hello\rworld"` | `@Khello[13]world` | `{s:"hello\rworld"}` |
+------------------+--------------------+----------------------+

# [Shockwave (Incoming)](#tab/string-shockwave-in)

+------------------+---------------------+----------------------+
| Value            | Legacy              | Expression           |
+==================+=====================+======================+
| `""`             | `[2]`               | `{s:""}`             |
+------------------+---------------------+----------------------+
| `"hi"`           | `hi[2]`             | `{s:"hi"}`           |
+------------------+---------------------+----------------------+
| `"hello"`        | `hello[2]`          | `{s:"hello"}`        |
+------------------+---------------------+----------------------+
| `"hello world"`  | `hello world[2]`    | `{s:"hello world"}`  |
+------------------+---------------------+----------------------+
| `"hello\tworld"` | `hello[9]world[2]`  | `{s:"hello\tworld"}` |
+------------------+---------------------+----------------------+
| `"hello\rworld"` | `hello[13]world[2]` | `{s:"hello\rworld"}` |
+------------------+---------------------+----------------------+

---

## Shockwave data types

### B64

A B64 represents a 12-bit unsigned fixed-length radix-64 encoded integer. Each character contains 6
bits of information, which can each represent $2^6=64$ unique values (including $0$, ranges from $0$
to $63$).

A B64 is always 2 bytes long, which means that with 6 bits of information in each byte, the range of
a B64 is from $0$ to $2^{12}-1=4095$.

Let's look at the number `70` which is `AF`, or `01000001` `01000110` in binary when encoded to B64.

The first 2 bits of each byte are always `01`:

```txt
    A       F
01000001 01000110
^^------ ^^------
 Always 01
```

This means that the range of a single byte can be from `01000000` to `01111111` in binary, `0x40` to
`0x7f` in hex, or from $64$ to $127$ in decimal. This entire range can be represented by a printable
ASCII character with the exception of the final value, which is represented by the legacy packet
format as `[127]`. See the right half of
[this ASCII table](https://en.m.wikipedia.org/wiki/File:ASCII-Table-wide.svg) for a visual
representation.

The remaining 6 bits of each byte contribute to its value:

```txt
    A       F
01000001 01000110
--^^^^^^ --^^^^^^
      Value
```

The bits from the second byte are shifted onto the left of the value. We can see that the bits from
the 2nd byte contribute to the most significant bits of the value:

```txt
      6 bits from
       1st byte
        vvvvvv
  000001000110 <- Value
  ^^^^^^
6 bits from
 2nd byte
```

Where the `000001` from the 2nd byte has the value of $64$ and the `000110` from the 1st byte has
the value of $6$, which when added together equals $70$.

See [short](#short) for its packet representations.

### VL64

A VL64 represents a signed variable-length radix-64 encoded integer. Like the B64, each character in
a VL64 also contains 6 bits of information.

Let's look at the number `38` which is `RI`, or `01010010` `01001001` in binary when encoded to
VL64.

The first 2 bits of each byte are always `01`:

```txt
    R       I
01010010 01001001
^^------ ^^------
 Always 01
```

The next 3 bits of the first byte indicate the byte length of the VL64, which allows it to have a
variable length.

```txt
01010010 01001001
--^^^--- --------
Length in bytes = 2
```

Since we have `010` which equals 2, this VL64 is 2 bytes long.

The next bit indicates the sign, if it is `1` then the number represents a negative value.

```txt
01010010 01001001
-----^-- --------
Sign bit, 0 = positive
```

The remaining bits hold the value of the number. Since the first byte only has 2 bits left, the
maximum value that a VL64 can represent with one byte is $2^2-1=3$.

```txt
01010010 01001001
------^^ --^^^^^^
       Value
```

The 6 bits from each consecutive byte are shifted onto the left of the value.

```txt
  1st byte
  01010010
        ||
        vv
  00100110 <- Value
  ^^^^^^
   \\\\\\
  01001001
  2nd byte
```

We can see above that the value of the number is `00100110` in binary, or $38$ in decimal.

Since there are 3 bits for the length, theoretically the maximum value using 7 bytes
($2+6*6=38$ bits) would be $2^{38}-1=274877906943$, however, from my testing on the Origins server,
the VL64 is backed by a signed 32-bit integer with a maximum value of $2147483647$ or `0x7fffffff`
(the "*sign*" bit is unused), making the effective range from $-2147483647$ to $2147483647$.

See [int](#int) for its packet representations.

## Xabbo data types

### Length

The `Length` data type is often written before a collection of items to specify the number of items
in the collection, such as the list of users or furni in the room. It was added due to how Unity
uses a [short](#short) in front of collections instead of an [int](#int). Therefore, it is written
as an [int](#int) on Flash and Shockwave, and as a [short](#short) on Unity.

If you read an array from a packet, a `Length` is read to determine the number of items to read:

```csharp
var values = e.Packet.Read<int[]>();
```

The above code will first read a `Length`, followed by a number of integers specified by the
length, and then return the resulting array of integers.

### Id

The `Id` data type represents a unique ID of a user, group, furni, etc. and was added due to how
Unity changed these IDs from an `int` to a `long`. Therefore, an `Id` is read as an [int](#int) on
Flash and Shockwave, and as a [long](#long) on a Unity session.

### PacketContent

The @Xabbo.PacketContent data type is specific to the Shockwave client, and was added due
to how some packet structures on Shockwave use their entire data as a string of characters, which
means there is no length header, nor terminator byte for incoming packets.

Often you will see a packet such as the following:

```txt
[ADDSTRIPITEM]
Outgoing[67] -> ACnew stuff 49848964
```

Where `AC` is the header value `67`, and `new stuff 49848964` is the packet data. We can see that
there is no length value at the beginning to indicate the number of characters in the string
`"new stuff 49848964"`, as seen with other [strings](#string). This is because the entire data of
the packet is being used as a string, so there is no need for a length header. Similarly, on an
incoming packet, you would not see the string terminated with a `[2]` byte.

To read or write a `PacketContent`, the packet's position must be at the start of the packet, after
which the position will be advanced to the end of the packet. Attempting to read or write a
`PacketContent` when the position is not at $0$ will throw an exception.

[^1]: `a0` encodes to the [non-breaking space](https://en.wikipedia.org/wiki/Non-breaking_space).
[^2]: G-Earth represents shorts as unsigned 16-bit integers (ranging from `0` to `65535`), while
xabbo uses signed 16-bit integers which allow negative values (ranging from `-32768` to `32767`).
However, `-1` and `65535` are represented exactly the same when encoded to 16 bits.
[^3]: See *2.* above. The value `32768` of an unsigned 16-bit integer and the negative value
`-32768` of a signed 16-bit integer encode to the exact same bytes: `80 00`. This is known as
[two's complement](https://en.wikipedia.org/wiki/Two%27s_complement).
[^4]: This is the number at which Builder's Club furni IDs start at: `2147418112` or `0x7fff0000`.