# Joker-Games-Case-Study

Unity Game Developer Case Study
Objective: Create a 3D Unity game that combines elements of inventory
management, dynamic map generation and interactive dice mechanics. Drawing
inspiration from games like "Monopoly GO," "Dice Dreams," and "Board Kings," the
game will offer an engaging board game experience in a three-dimensional space.
Features:
1. Inventory System:
● Items: Players can collect three types of items: apples, pears, and
strawberries.
● Persistence: Quantities of these items are saved even when the game is
closed and restored upon reopening to maintain inventory.
● UI Display: The player’s current inventory is displayed prominently in the
top-right corner of the game screen.
2. 3D Map Generation:
● Creation: The game map is either randomly generated or created from JSON
data to produce a three-dimensional board that players navigate.
● Map Elements: Tiles on the map can be designated as empty or contain
items (e.g., 5 apples, 3 pears, 8 pears, empty, 15 strawberries).
● Visualization: Each tile is represented in 3D with clear numbering and item
rewards displayed on map steps
3. Dice Mechanics:
● Input and Display: Players input dice values via two textboxes located in the
upper-left corner of the interface. Labels next to these boxes clarify their
purpose.
● Animation and Interaction: Dice animations occur in a 3D space in front of
the player, with numbers corresponding to player input. This mimics real-life
dice throwing, enhancing immersion.
● Movement: The sum of the dice determines how many tiles the player
advances on the 3D board, with landed tiles number logged and items
collected automatically added to the inventory. If the player's movement
exceeds the number of tiles on the map, player will wrap around to the
beginning and continue their turn from the first tile.
● PLUS (Optional): A dropdown menu allows players to choose the number of
dice they want to roll, altering the number of textboxes displayed accordingly
for input. (Dropdown possible values : 1-2-3-4-5-6-…- 50)
4. Additional Features: (HUGE PLUS)
● Particle Effects and Animations: Utilize high-quality 3D animations and
particle effects for actions like collecting items or rolling dice, significantly
boosting the game's visual appeal.
● Creative Freedom: Developers are encouraged to introduce unique and
imaginative elements into the game to enhance player engagement and
provide a visually rich gaming experience.
Evaluation Criteria:
● Clear, reusable, modular code and architecture.
● Mastery of OOP and SOLID principles, concepts and abstractions.
● Responsiveness and visual quality of the dice mechanics and their impact on
game progression.
● Implementation of creative and effective animations and particle effects.
Technologies
● Engine : Unity (2022+)
● Programming Language : C#
● Version Control : Git