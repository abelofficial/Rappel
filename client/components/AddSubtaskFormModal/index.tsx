import React, { useContext, useState } from "react";
import { Form, Formik } from "formik";
import {
  initialValues,
  validationSchema,
  fieldNames,
  fieldLabels,
} from "./addSubtaskFormProps";
import { CreateSubtaskCommand, SubtaskResponseDto } from "../../types";
import {
  Button,
  Card,
  Grid,
  Modal,
  Text,
  Tooltip,
  useTheme,
} from "@nextui-org/react";
import TextField, { TextAreaField } from "../TextField";
import { AuthContext, AuthContextInterface } from "../../Contexts/Auth";
import { createSubtaskCommand } from "../../services/commands";
import { mutate } from "swr";

export interface AddSubtaskFormModal {
  todoId: number;
}

const Index = ({ todoId }: AddSubtaskFormModal) => {
  const { theme } = useTheme();
  const [visible, setVisible] = React.useState(false);

  const handler = () => setVisible(true);

  const closeHandler = () => {
    setVisible(false);
  };
  const [errors, setErrors] = useState<string[] | undefined>();
  const { token } = useContext<AuthContextInterface>(AuthContext);

  const onSubmitHandler = async (values: CreateSubtaskCommand) => {
    try {
      mutate(
        `/todo/${todoId}/todossubtasks`,
        async (data: SubtaskResponseDto[]) => {
          const newSubTask = await createSubtaskCommand(
            token + "",
            todoId,
            values
          );
          return [...data, newSubTask];
        }
      );
      setVisible(false);
    } catch (e: any) {
      setErrors(e?.response?.data?.errors);
    }
  };

  const displayErrors = () => (
    <Tooltip content='' contentColor='error'>
      <Grid.Container alignItems='center' gap={1}>
        {errors?.map((e) => (
          <Grid key={e} xs={12}>
            <Text color='error'>{e}</Text>
          </Grid>
        ))}
      </Grid.Container>
    </Tooltip>
  );
  return (
    <Grid.Container
      gap={2}
      css={{
        width: "fit-content",
        backgroundColor: theme?.colors.backgroundContrast,
      }}
    >
      <Grid xs={12}>
        <Button auto color='success' size='sm' shadow onClick={handler}>
          Add subtask
        </Button>
      </Grid>
      <Modal
        closeButton
        blur
        aria-labelledby='modal-title'
        open={visible}
        onClose={closeHandler}
      >
        <Modal.Header>
          <Text id='modal-title' size={18}>
            Create new{" "}
            <Text b size={18}>
              subtask
            </Text>
          </Text>
        </Modal.Header>
        <Modal.Body>
          <Formik
            initialValues={initialValues}
            onSubmit={onSubmitHandler}
            validationSchema={validationSchema}
          >
            {({ isSubmitting }) => (
              <Form>
                <Card variant='bordered'>
                  <Grid.Container
                    gap={1}
                    justify='center'
                    alignItems='center'
                    css={{
                      backgroundColor: theme?.colors.primaryShadow,
                      border: theme?.borderWeights.light,
                      boxShadow: theme?.shadows.lg,
                      padding: theme?.space.lg,
                    }}
                  >
                    {errors && displayErrors()}
                    <TextField
                      type='text'
                      name={fieldNames.title}
                      label={fieldLabels.title}
                      fullWidth
                    />
                    <TextAreaField
                      type='password'
                      name={fieldNames.description}
                      label={fieldLabels.description}
                      placeholder='Enter your amazing ideas.'
                      fullWidth
                    />

                    <Grid xs={12}>
                      <Button
                        type='submit'
                        color='success'
                        disabled={isSubmitting}
                      >
                        Create
                      </Button>
                    </Grid>
                  </Grid.Container>
                </Card>
              </Form>
            )}
          </Formik>
        </Modal.Body>
      </Modal>
    </Grid.Container>
  );
};

export default Index;
