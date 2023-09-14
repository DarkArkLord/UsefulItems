function PreciseCalculations(numerator, denominator) {
    this.numerator = numerator || 0;
    this.denominator = denominator || 1;
}

PreciseCalculations.prototype = {
    // Get value
    toString() {
        return `(${this.numerator} / ${this.denominator})`;
    },
    valueOf() {
        return this.numerator / this.denominator;
    },
    round(n) {
        return +this.valueOf().toFixed(n);
    },
    sign() {
        return Math.sign(this.valueOf());
    },
    // Copy
    copy() {
        return new PreciseCalculations(this.numerator, this.denominator);
    },
    // Simplify
    simplify() {
        function findGreatestCommonDivisor(a, b) {
            let t;
            while (b > 0) {
                t = a % b;
                a = b;
                b = t;
            }
            return a;
        }

        let num = Math.abs(this.numerator);
        let den = Math.abs(this.denominator);
        let gcd = findGreatestCommonDivisor(num, den);
        while (gcd > 1) {
            num /= gcd;
            den /= gcd;
            gcd = findGreatestCommonDivisor(num, den);
        }
        this.numerator = num * this.sign();
        this.denominator = den;
        return this;
    },
    simplifyCopy() {
        return this.copy().simplify();
    },
    // Math
    reverse() {
        return new PreciseCalculations(this.denominator, this.numerator);
    },
    negative() {
        return new PreciseCalculations(-this.numerator, this.denominator);
    },
    add(other) {
        if (this.denominator == other.denominator) {
            let numerator = this.numerator + other.numerator;
            return new PreciseCalculations(numerator, this.denominator);
        }
        let num1 = this.numerator * other.denominator;
        let num2 = other.numerator * this.denominator;
        let den = this.denominator * other.denominator;
        return new PreciseCalculations(num1 + num2, den);
    },
    sub(other) {
        return this.add(other.copy().negative());
    },
    mul(other) {
        let num = this.numerator * other.numerator;
        let den = this.denominator * other.denominator;
        return new PreciseCalculations(num, den);
    },
    div(other) {
        return this.mul(other.copy().reverse());
    },
    // Number math
    addNumber(other) {
        let otherFraction = PreciseCalculations.fromNumber(other);
        return this.add(otherFraction);
    },
    subNumber(other) {
        let otherFraction = PreciseCalculations.fromNumber(other);
        return this.sub(otherFraction);
    },
    mulNumber(other) {
        let otherFraction = PreciseCalculations.fromNumber(other);
        return this.mul(otherFraction);
    },
    divNumber(other) {
        let otherFraction = PreciseCalculations.fromNumber(other);
        return this.div(otherFraction);
    },
    // Compare
    equals(other) {
        let a = this.simplifyCopy();
        let b = other.simplifyCopy();
        return a.numerator == b.numerator
            && a.denominator == b.denominator;
    }
};

PreciseCalculations.fromNumber = (number, accuracy = 6) => {
    if (!number || Number.isNaN(number)) {
        return PreciseCalculations.zero();
    }
    if (Number.isInteger(number)) {
        return new PreciseCalculations(number, 1);
    }
    let sign = Math.sign(number);
    let numberAbs = Math.abs(number);
    let numbersCount = numberAbs.toString().split('.').pop().length;
    if (numbersCount > accuracy) {
        numbersCount = accuracy;
    }
    let denominator = 10 ** numbersCount;
    let numerator = sign * Math.round(numberAbs * denominator);
    let result = new PreciseCalculations(numerator, denominator);
    return result;
};

PreciseCalculations.fromObject = (obj) => {
    let num = getValue(obj, 'numerator', 0);
    let den = getValue(obj, 'denominator', 1);
    let result = new PreciseCalculations(num, den);
    return result;
};

PreciseCalculations.zero = () => new PreciseCalculations(0, 1);