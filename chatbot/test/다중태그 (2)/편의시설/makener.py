import re

import pandas as pd
from konlpy.tag import Komoran


def read_text_data(filename):
    with open(filename, 'r',encoding="utf8") as f:
        data = [line for line in f.read().splitlines()]
        # data = [line for line in f.readlines()]
        # data = data[1:] #헤더 제거 헤더가 없다
    return data
def read_tag_data(filename):
    with open(filename, 'r',encoding="utf8") as f:
        dic ={}
        for data in f.read().splitlines():
            temp = data.split('\t')
            dic[temp[0]]= temp[1]

    return dic

taging = read_tag_data("./단어정리.txt")
corpus_data = read_text_data('./TotalAllText.txt') #본인 데이터셋
komoran = Komoran(userdic='./user_dic.tsv') #사용자정의 딕셔너리 폴더
f = open("Ner_train.txt", "w")

for c in corpus_data:
    TF = True
    pos = komoran.pos(c.upper())
    firstline = "; {}\n".format(c)
    f.write(firstline)
    b = []
    for a in pos:
        b.append(a[0])
    for k, v in taging.items():
        if k in b  and TF is True:
            print(c+k+"\n")
            parse = re.sub(k, "", c)
            secondline = re.sub(k,("<{}:{}>".format(k,v)),c)
            temp= "$ {}\n".format(secondline)
            f.write(temp)
            idx = 1
            for tagdata in pos:
                    if tagdata[0] == k:
                        idxline = "{}\t{}\t{}\tB_{}\n".format(idx, tagdata[0], tagdata[1], v)
                        idx += 1
                        f.write(idxline)
                    else:
                        idxline = "{}\t{}\t{}\tO\n".format(idx, tagdata[0], tagdata[1])
                        idx += 1
                        f.write(idxline)
            f.write("\n")
            TF = False
            idx = 1





f.close()









'''text = "우리 w1 엔엘피를 좋아해."
pos = komoran.pos(text)
print(pos)'''