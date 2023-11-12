const alphabet = "abcdefghijklmnopqrstuvwxyz";

function encrypt(text: string, shift: number): string {
    let encryptedText = "";
    for (const letter of text) {
        const index = alphabet.indexOf(letter);
        if (index === -1) {
            encryptedText += letter;
        } else {
            encryptedText += alphabet[(index + shift) % 26];
        }
    }
    return encryptedText;
}

function decrypt(text: string, shift: number): string {
    return encrypt(text, -shift);
}

let numberOfRuns = 0;

function checkNumberOfRuns() {
    numberOfRuns++;
    if (numberOfRuns > 5) {
        console.log("Program has been launched too many times.");
        process.exit(1);
    }
}

function main() {
    checkNumberOfRuns();

    const key = "password";
    const encryptedKey = encrypt(key, 3);

    console.log("Encrypted key:", encryptedKey);

    const decryptedKey = decrypt(encryptedKey, 3);
    console.log("Decrypted key:", decryptedKey);
}

main();

export { };
