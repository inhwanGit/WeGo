


def read_text_data(filename):
    with open(filename, 'r') as f:
        data = f.readlines()
        # data = [line for line in f.readlines()]
        # data = data[1:] #헤더 제거 헤더가 없다
        return data


corpus_data = read_text_data('Ner_train.txt') #본인 데이터셋

idx = 0
check = 0

for line ,data in enumerate(corpus_data):
    idx = idx+1
    if data[0] == ';':
        check = check+1
    elif data[0] =='$':
        check = 0

    if check ==2:
        print(idx-1)



