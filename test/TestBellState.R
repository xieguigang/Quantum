let numOnesQ1 = 0;
let numOnesQ2 = 0;

let count = 1000;
let q = Qubit(2);
let initial = 1;
let Zero = 0;
let One = 1;

SetQubitState = function(desired, target) {
    if (desired != M(target)) {
        X(target);
    }
}

for(test in 1:count) {
    SetQubitState([initial, Zero]);

    # measure each qubit
    let result = M(q1);            

    # Count the number of 'Ones':
    if (result[1] == One) {
        numOnesQ1 = numOnesQ1 + 1;
    }
    if (result[2] == One) {
        numOnesQ2 = numOnesQ2 + 1;
    }
}

# reset the qubits
SetQubitState([Zero, Zero]);

# Return number of |0> states, number of |1> states
print("q1:Zero, One  q2:Zero, One");
print([count - numOnesQ1, numOnesQ1, count - numOnesQ2, numOnesQ2 ]);

# q1:Zero, One q2:Zero, One
# (1000, 0, 1000, 0)