# CardChauffeur

1. This is a console application-type project
2. It is a console based graphical user interface
3. The entry class for this application is Program(Program.cs)

# RandomCardGenerator

1. This project is a library-type project that contains the business logic of the game.
2. State Design pattern is used
3. Please have a look at the state transistion diagram (inside Design folder) to understand the state transistions of the game before looking at the state machine code
4. The entry class for this library is RandomCardGenerator (RandomCardGenerator.cs), which load StateMachine.

# Logger

1. This project is a library-type project that contains logging and recovery management logic.
2. This library is used for logging, saving the state of the game and recovering it
3. This has a static implementation of classes 

# About the game

1. User can draw a card from a shuffled deck of 52 cards
2. User can reshuffle the deck
3. User can reset the game where it will reshuffle the deck along with the drawn card
4. User can save the state of the game and later recover it

