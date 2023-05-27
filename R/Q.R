#' measures qubit, returns result (and resets it to |0> before deallocation)
#' 
#' @param q a qubit
#' 
const MResetZ = function(q) {
    let result = M(q);
    X(q);
    return(result);
}