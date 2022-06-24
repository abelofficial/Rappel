import { CreateSubtaskCommand } from "../../types";
import { string, SchemaOf, object } from "yup";

export const fieldLabels: CreateSubtaskCommand = {
  title: "Title",
  description: "Description",
};

export const fieldNames: CreateSubtaskCommand = {
  title: "title",
  description: "description",
};

export const initialValues: CreateSubtaskCommand = {
  title: "",
  description: "",
};

export const validationSchema: SchemaOf<CreateSubtaskCommand> = object({
  title: string().required("Title is required"),
  description: string()
    .required("Description is required")
    .min(20, "Description must be min 20 characters."),
});
