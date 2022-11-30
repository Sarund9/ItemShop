
 
# This script was used to Crop all of the LPC Sprites, to only get the Walk Cycle

# Then it was used to Pick color of all 
 
 # This requires the Pillow library
 # pip install pillow
 
from os import *
from PIL import Image
from shutil import copy
from re import match


def cut(fpath: str):
    """
    Loads a PNG, applies a cut, and Replaces it
    """
    if not path.exists(fpath) or not fpath.endswith('.png'):
        return
    return # for security

     # Create backup
    copy(fpath, "Backups")
    
     # This cuts the Walk Cycle from the Animation
    box = (0, 8 * 64, 9 * 64, 12 * 64)
    
    try:
        img = Image.open(fpath)
        img.crop(box).save(fpath)
    finally:
        img.close()

def restore(fpath: str):
    bfile = fpath + "_backup.backup"
    if not path.exists(bfile):
        return
    
    copy(bfile, fpath)

 # Image Cutting
 # Bottom: 9 x 64
 # Top: 8 x 64
 # Right: 4 x 64
 # (left, upper, right, lower)
# dimensions = (0, 8 * 64, 4 * 64, 9 * 64)


# run_on = [
#     ""
# ]

# cut("Character/character.png")



def all_images(dir: str):
    if not path.isdir(dir):
        print("No Directory")
        return

    for root, subdirs, files in walk(dir):
        
        # print("==========================")
        # print("root: ", root)
        # print("subdirs: ", subdirs)
        # print("files: ", files)
        # print("")
        for f in files:
            fpath = path.join(root, f)
            # print(" > ", fpath)
            if fpath.endswith('.png'):
                yield fpath
    
    # for fname in listdir(dir):
    #     f = path.join(dir, fname)
    #     print("FILE: ", f)
    #     if f.endswith('.png') and path.isfile(f):
    #         yield f
        
def all_fem_dirs(dir: str):
    if not path.isdir(dir):
        print("No Directory")
        return
    
    for root, subdirs, files in walk(dir):
    
        for sd in subdirs:
            fpath = path.join(root, sd)
            if sd == "female":
                yield fpath
    
        # for f in files:
        #     fpath = path.join(root, f)
        #     # print(" > ", fpath)
        #     if fpath.endswith('.png'):
        #         yield fpath

def meta_of(path: str) -> str:
    return path + ".meta"

def umove(file: str, to: str):
    """
    Moves a file from 'file' -> 'to', and it's .meta file
    """
    if not path.exists(file) or path.exists(to):
        return False
    
    try:
        rename(file, to)
        rename(meta_of(file), meta_of(to))
        return True
    except:
        return False


def try_int(txt: str):
    try:
        return int(txt)
    except:
        return None

def go_up(dir: str):
    return path.normpath(path.join(dir, ".."))

def last_folder(dir: str):
    return 

def pick(dir: str):
    """
    Picks 'white.png' from directory, copies it outside that directory, then deletes the directory
    """
    
    # TODO: Ignore Meta Files
    # TODO: None Option
    # TODO: Auto Find white.png
    
    files = listdir(dir)
    
    
    
    white: str = None
    for f in files:
        if f == 'white.png':
            white = f
    
    if white is None:
        return
    
    file = f"{dir}\\{white}"
    to = go_up(dir) + ".png"
    
    
    umove(file, to)
    
    # print(f"Copy: {dir}\\{white} \n >>> {to}")
    
    
    # print("")
    # print("FILE FOR: ", dir)
    # i = 0
    # for file in files:
    #     print(" <", i, "> ", file)
    #     i += 1
        
    # print("Pick > ")
    # inp = input()
    # num = try_int(inp)
    # if num is None:
    #     return
    # if num > -1 and num < len(files):
    #     print("You Chose: ", files[num])
    # else:
    #     print("Invalid File")
    

# print("Crop all Images")
for d in all_fem_dirs("Character/Clothes"):
    pick(d)

# print("")
# print("Finished")
# print("")
