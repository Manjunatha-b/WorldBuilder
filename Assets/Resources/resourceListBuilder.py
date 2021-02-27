import os
import numpy as np

def readDir(path):
    dirPaths = []
    dirs = []
    for item in os.listdir(path):
        if(os.path.isdir(path+item)):
            dirPaths.append(path+item+"/")
            dirs.append(item)
    return dirs,dirPaths

def readItems(path):
    itemPaths = []
    for item in os.listdir(path):
        if(os.path.isdir(path+item+"/")):
            itemPaths.append(path+item)
    return np.array(itemPaths)


def createLookup(assetType):
    savePath = "ResourceList/"+assetType+"/"
    os.mkdir(savePath)
    dirs, dirPaths = readDir("Prefabs/"+assetType+"/")
    for i in range(len(dirs)):
        itemList = readItems(dirPaths[i])
        np.savetxt(savePath+dirs[i]+".txt",itemList,fmt="%s")
    np.savetxt(savePath+"main.txt",dirs,fmt="%s")

def createLookups():
    os.mkdir("ResourceList/")
    for item in os.listdir("Prefabs/"):
        if(os.path.isdir("Prefabs/"+item)):
            createLookup(item)

createLookups()




