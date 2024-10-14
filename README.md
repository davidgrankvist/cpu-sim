# cpu-sim
CPU simulation

## About

This simulation is essentially an interpreter to execute a custom assembly language dialect. As the assembly is executed, a simplified CPU state is updated.

Here's how it works:
- the interpreter parses assembly instructions into commands
- commands are sent to the executor
- the executor executes the next command specified by the program counter
- the commands mutate the CPU state like registers and the call stack
- I/O devices interact with the CPU using mapped memory addresses

### Code Examples

#### Subroutines

Here is a program that calls a subroutine to add two numbers together. The registers are prefixed with `r`.

```assembly
main:
    ld r0 10
    psh r0
    ld r0 2
    psh r0
    call addSubroutine
    pop r0
    jmp end

addSubroutine:
    pop r0
    pop r1
    add r0 r1
    psh r0
    ret

end:
```

#### I/O

In this example, an input device writes to address `0x1337` and an output device reads from address `0x1338`. The devices run on separate threads and the CPU polls for input continuously.

```assembly
loop:
    lda r0 0x1337
    # do some processing
    st r0 0x1338
    jmp loop
```
