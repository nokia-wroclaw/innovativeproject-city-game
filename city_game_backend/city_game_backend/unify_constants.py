import CONSTANTS
from sys import platform

if platform != "linux" and platform != "linux2":
    print('THIS CAN ONLY RUN ON LINUX BECAUSE I DON\'T HAVE TIME TO FIX THE WINDOWS PATH SYSTEM')
    exit(2137)

"""
    We need to have the same constants file in both Python and C# so I built a simple converter.

    It takes the CONSTANTS.py Python file and converts it into a C# class, then saves it.
"""

# Let's start from an empty class
generated_code = '''/**
 * @file contains all const data used in the aplication
 * 
 * 
 * 
 */
public static class Const
{

'''

# Get all the *normal* variables in the file
variables =  [variable for variable in dir(CONSTANTS) if not variable.startswith("_")]

for variable in variables:
    value = getattr(CONSTANTS, variable)
    
    generated_code += '\tpublic static '
    if isinstance(value, str):
        generated_code += f'string {variable} = "{value}";\n'
    elif isinstance(value, int):
        generated_code += f'int {variable} = {value};\n'
    elif isinstance(value, float):
        generated_code += f'float {variable} = {value}F;\n'

# Close the class, we're done
generated_code += '''
}
'''

with open('../../city_game_frontend/Assets/Scripts/Const.cs', 'w') as out_file:
    out_file.write(generated_code)
