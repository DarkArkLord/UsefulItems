function getRange(cnt) {
  return Array.from(Array(cnt));
}

const letters = getRange('Z'.charCodeAt(0) - 'A'.charCodeAt(0) + 1)
  .map((_, i) => String.fromCharCode('A'.charCodeAt(0) + i))
  .join('');

const startIndex = 18;

function numberToLetters(number) {
  if (number < letters.length) {
    // return `${number} -> ${letters[number]}`;
    return letters[number];
  }

  const a = Math.floor(number / letters.length);
  const b = number % letters.length;
  // return `${number} -> ${a}.${b} -> ${letters[a - 1] + letters[b]}`;
  return letters[a - 1] + letters[b];
}

const values = getRange(26)
  .flatMap((_, index) => {
    const number = startIndex + index * 3;
    return [
      {
        index: numberToLetters(number),
        name: `cov.${index}.SumInsured`,
      },
      {
        index: numberToLetters(number + 1),
        name: `cov.${index}.Premium`,
      },
      {
        index: numberToLetters(number + 2),
        name: `cov.${index}.PayOrRefund`,
      },
    ]
  });

console.log(JSON.stringify(values))