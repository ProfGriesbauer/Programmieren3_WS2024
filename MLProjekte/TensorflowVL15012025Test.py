import tensorflow.compat.v1 as tf
import numpy as np
tf.disable_v2_behavior()

a = tf.Variable(3, name='var-a')
b = tf.Variable(4, name='var-b')
c = tf.placeholder(tf.int32, shape=[], name='pla-c')
d = tf.multiply(a, b, name='mult-op-d')
e = tf.add(c, d, name='add-op-e')
with tf.Session() as my_session:
	my_session.run(tf.global_variables_initializer())
	print(my_session.run(e, {c:5}))
