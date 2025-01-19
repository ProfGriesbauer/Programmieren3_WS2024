import numpy as np
import mnist
import tensorflow.keras as keras
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import Conv2D, MaxPooling2D, Dense, Flatten
from tensorflow.keras.utils import to_categorical

(train_images, train_labels), (_, _) = keras.datasets.mnist.load_data()

num_filters = 8
filter_size = 3
pool_size = 2

print(to_categorical(train_labels))

#### MODEL ####
model = Sequential()
model.add(Conv2D(num_filters, filter_size, input_shape=(28,28,1)))
model.add(MaxPooling2D(pool_size=pool_size))
model.add(Flatten())
model.add(Dense(10, activation='sigmoid'))

model.compile('adam', loss='mean_squared_error', metrics=['accuracy'])
model.fit(train_images, to_categorical(train_labels), epochs=3)

print(model.predict(train_images, batch_size=1))