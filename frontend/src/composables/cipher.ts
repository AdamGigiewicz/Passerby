const alphabet = "abcdefghijklmnopqrstuvwxyz";

function encrypt(text: string, shift: number): string {
    let encryptedText = "";
    for (const letter of text) {
        const index = alphabet.indexOf(letter);
        if (index === -1) {
            encryptedText += letter;
        } else {
            encryptedText += alphabet[(index + shift + 26) % 26]; 
        }
    }
    return encryptedText;
}

function decrypt(text: string, shift: number): string {
    return encrypt(text, -shift);
}

export {encrypt, decrypt };
