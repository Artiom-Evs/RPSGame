# RPSGame #

## Overview #

Console game similar to Rock-Scissors-Paper with configurable moves functionality and cryptographic confirmation of the PC's moves. 
Develops as a task project for an internship of the Itransition company .

## Reduction of requirements #

Develop console game similar to Rock-Slicers-Paper.

Allows to play the game Rock-Slicers-Paper with PC with configurable move variants. 

Functional requirements: 
1. Move variants are configured as a command line arguments.
2. Number of the move variants must be odd and more or equal three.
3. Move variants cannot be duplicated.
4. Victory or defeat is computed by sequence of a command line arguments: next half of move variants loses current move variant, and previous half wins current move variant.
5. Player is asked to choose move variant, exit or help during his move. 
6. PC's move confirms by cryptographic sign: PC generates secret key, computes signature for his move and shows signature to player. PC shows secret key after player's move. It is allows to make sure that the PC is not change his move variant.
7. User interface must be in English.

Non-functional requirements: 
1. Cryptographic signature must be computed by HMAC SHA2 or SHA3 algorithm.
2. Secret key length must be more or equal 256 bits. 

## Project structure #

Logic entities:
1. TUI
2. game context
3. Data signer
4. Key generator

Program entities:

Interfaces:
1. IKeyGenerator
2. IDataSigner

Classes:
1. TUI
2. GameContext
3. BaseKeyGenerator
4. HMACSHA2DataSigner

Enums:
1. RoundResults
