exports.getOptions = function (model) {
  return {
    isShared: true
  }
}

exports.transform = function (model) {
  return model;
}